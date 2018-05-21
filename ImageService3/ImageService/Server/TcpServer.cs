﻿using ImageService.Controller;
using ImageService.Logging;
using ImageService.Logging.Modal;
using ImageService.Modal;
using ImageService.Server;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ImageService.Server
{
    public class TcpServer : ITcpServer
    {
        ILoggingService Logging { get; set; }
        int Port { get; set; }
        TcpListener Listener { get; set; }
        IClientHandler Ch { get; set; }
        private List<TcpClient> clients = new List<TcpClient>();
        private static Mutex m_mutex = new Mutex();

        /// <summary>
        /// ImageServiceSrv constructor.
        /// </summary>
        /// <param name="port">port num</param>
        /// <param name="logging">ILoggingService obj</param>
        /// <param name="ch">IClientHandler obj</param>
        public TcpServer(int port, ILoggingService logging, IClientHandler ch)
        {
            this.Port = port;
            this.Logging = logging;
            this.Ch = ch;
            ClientHandler.Mutex = m_mutex;
        }

        /// <summary>
        /// Start function.
        /// lissten to new clients.
        /// </summary>
        public void Start()
        {
            try
            {
                IPEndPoint ep = new
                IPEndPoint(IPAddress.Parse("127.0.0.1"), Port);
                Listener = new TcpListener(ep);

                Listener.Start();
                Logging.Log("Waiting for client connections...", MessageTypeEnum.INFO);
                Task task = new Task(() =>
                {
                    while (true)
                    {
                        try
                        {
                            TcpClient client = Listener.AcceptTcpClient();
                            Logging.Log("Got new connection", MessageTypeEnum.INFO);
                            clients.Add(client);
                            Ch.HandleClient(client, clients);
                        }
                        catch (Exception ex)
                        {
                            break;
                        }
                    }
                    Logging.Log("Server stopped", MessageTypeEnum.INFO);
                });
                task.Start();
            }
            catch (Exception ex)
            {
                Logging.Log(ex.ToString(), MessageTypeEnum.FAIL);
            }
        }

        /// <summary>
        /// Stop func.
        /// stop listen to new clients.
        /// </summary>
        public void Stop()
        {
            Listener.Stop();
        }

        /// <summary>
        /// NotifyAllClientsAboutUpdate function.
        /// notifies all clients about update (new log, handler deleted).
        /// </summary>
        /// <param name="commandRecievedEventArgs"></param>
        public void NotifyAllClientsAboutUpdate(CommandRecievedEventArgs commandRecievedEventArgs)
        {
            try
            {
                List<TcpClient> copyClients = new List<TcpClient>(clients);
                foreach (TcpClient client in copyClients)
                {
                    new Task(() =>
                    {
                        try
                        {
                            NetworkStream stream = client.GetStream();
                            BinaryWriter writer = new BinaryWriter(stream);
                            string jsonCommand = JsonConvert.SerializeObject(commandRecievedEventArgs);
                            m_mutex.WaitOne();
                            writer.Write(jsonCommand);
                            m_mutex.ReleaseMutex();
                        }
                        catch (Exception ex)
                        {
                            this.clients.Remove(client);
                        }

                    }).Start();
                }
            }
            catch (Exception ex)
            {
                Logging.Log(ex.ToString(), MessageTypeEnum.FAIL);
            }
        }

        /// <summary>
        /// NotifyAboutNewLogEntry function.
        /// </summary>
        /// <param name="updateObj">CommandRecievedEventArgs obj</param>
        private void NotifyAboutNewLogEntry(CommandRecievedEventArgs updateObj)
        {
            NotifyAllClientsAboutUpdate(updateObj);
        }
    }

}