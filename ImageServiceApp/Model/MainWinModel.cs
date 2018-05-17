﻿using ImageServiceApp.Communication;
using ImageServiceApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceApp.Model
{
    class MainWinModel: IMainWinModel
    {
        private bool m_isConnected;

        public IClient GuiClient { get; set; }

    

        public bool IsConnected
        {
            get { return m_isConnected; }
            set
            {
                m_isConnected = value;
                OnPropertyChanged("IsConnected");
            }
        }

        
        public MainWinModel()
        {
            GuiClient = Client.Instance;
            IsConnected = GuiClient.Connected;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// OnPropertyChanged function.
        /// defines what happens when property changed.
        /// </summary>
        /// <param name="name">prop name</param>
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
