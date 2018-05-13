﻿using System;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using Newtonsoft.Json;
using ImageServiceApp.Event;
using ImageServiceApp.Communication;

namespace ImageServiceApp.Models
{
    public class Client: IClient
    {
        private static Client model;
        private string appConfig;
        private string logs;
        private TcpClient client;
        private NetworkStream stream;
        private BinaryWriter writer;
        private BinaryReader reader;

        public event EventHandler<InfoEventArgs> InfoRecieved;

        public string AppConfig
        {
            get { return appConfig; }
            set { }
        }

        public string Logs
        {
            get { return logs; }
            set { }
        }

        public static Client Connection()
        {
            // if not already created
            if (model == null)
            {
                model = new Client();
            }
            // otherwise create new instance
            return model;
        }

        public void start()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            client = new TcpClient();
            client.Connect(ep);
            Reciever();
        }

        public void Sender(object sender, CommandRecievedEventArgs e)
        {
            new Task(() =>
            {
                stream = client.GetStream();
                writer = new BinaryWriter(stream);
                string args = JsonConvert.SerializeObject(e);
                writer.Write(args);
            }).Start();
        }

        private void Reciever()
        {
            new Task(() =>
            {
                stream = client.GetStream();
                reader = new BinaryReader(stream);
                while (client.Connected)
                {
                    string args;
                    try
                    {
                        args = reader.ReadString();
                    }
                    catch (Exception error)
                    {
                        return;
                    }
                    InfoEventArgs e = JsonConvert.DeserializeObject<InfoEventArgs>(args);
                    InfoRecieved?.Invoke(this, e);
                }
            }).Start();
        }

        public void Disconnect()
        {
            client.Close();
        }
    }
}