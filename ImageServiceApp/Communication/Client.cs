﻿using System;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using Newtonsoft.Json;
using ImageServiceApp.Event;
using System.Threading;
using ImageServiceApp.Enums;
using System.Windows;

namespace ImageServiceApp.Communication
{
    public class Client: IClient
    {
        private TcpClient client;
        private bool stopped;
        public delegate void UpdateResponseArrived(CommandRecievedEventArgs responseObj);
        public event Communication.UpdateResponseArrived UpdateResponse;
        private static Client clientInstance;
        private static Mutex mutex = new Mutex();
        public bool Connected { get; set; }

        /// <summary>
        /// ImageServiceClient private constructor.
        /// </summary>
        private Client()
        {
            this.Connected = this.Start();
        }

        /// <summary>
        /// Instance - returns instance of the singleton class.
        /// </summary>
        public static Client Instance
        {
            get
            {
                if (clientInstance == null)
                {
                    clientInstance = new Client();
                }
                return clientInstance;
            }
        }

        /// <summary>
        /// Start function.
        /// starts the tcp connection.
        /// </summary>
        /// <returns></returns>
        private bool Start()
        {
            try
            {
                bool result = true;
                string ip = "127.0.0.1";
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), 8000);
                client = new TcpClient();
                client.Connect(ep);
                Console.WriteLine("Succeed connected");
                stopped = false;
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// SendCommand function.
        /// sends command to srv.
        /// </summary>
        /// <param name="commandRecievedEventArgs">info to be sented to server</param>
        public void SendCommand(CommandRecievedEventArgs commandRecievedEventArgs)
        {
            new Task(() =>
            {
                try
                {
                    string jsonCommand = JsonConvert.SerializeObject(commandRecievedEventArgs);
                    NetworkStream stream = client.GetStream();
                    BinaryWriter writer = new BinaryWriter(stream);
                    // Send data to server
                    Console.WriteLine($"Send {jsonCommand} to Server");
                    mutex.WaitOne();
                    writer.Write(jsonCommand);
                    mutex.ReleaseMutex();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }).Start();
        }

        /// <summary>
        /// RecieveCommand function.
        /// creates task and reads new messages.
        /// </summary>
        public void RecieveCommand()
        {
            new Task(() =>
            {
                try
                {
                    while (!stopped)
                    {
                        NetworkStream stream = client.GetStream();
                        BinaryReader reader = new BinaryReader(stream);
                        string response = reader.ReadString();
                        Console.WriteLine($"Recieve {response} from Server");
                        CommandRecievedEventArgs responseObj = JsonConvert.DeserializeObject<CommandRecievedEventArgs>(response);
                        this.UpdateResponse?.Invoke(responseObj);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }).Start();
        }

       
        public void Disconnected()
        {
            CommandRecievedEventArgs commandRecievedEventArgs = new CommandRecievedEventArgs((int)CommandEnum.Disconnected, null, "");
            this.SendCommand(commandRecievedEventArgs);
            client.Close();
            this.stopped = true;
        }


    }
}