﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using ImageServiceApp.Event;
using ImageServiceApp.Enums;
using ImageServiceApp.Model;
using ImageServiceApp.Communication;
using ImageServiceApp.Models;
using System.Windows.Data;

namespace ImageServiceApp.Model
{
    class SettingsModel : ISettingsModel
    {
        public IClient GuiClient { get; set; }
        private string m_tumbnailSize;
        private string m_logName;
        private string m_outputDirectory;
        private string m_sourceName;

        /// <summary>
        /// SettingModel constructor.
        /// </summary>
        public SettingsModel()
        {
            this.GuiClient = Client.Instance;
            this.GuiClient.RecieveCommand();
            this.GuiClient.UpdateResponse += UpdateResponse;
            this.InitializeSettingsParams();
        }

        /// <summary>
        /// UpdateResponse function.
        /// updates the model when message recieved from srv.
        /// </summary>
        /// <param name="responseObj">the info came from srv</param>
        private void UpdateResponse(CommandRecievedEventArgs responseObj)
        {
            try
            {
                if (responseObj != null)
                {
                    switch (responseObj.CommandID)
                    {
                        case (int)CommandEnum.GetConfigCommand:
                            UpdateConfigurations(responseObj);
                            break;
                        case (int)CommandEnum.CloseHandler:
                            CloseHandler(responseObj);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// UpdateConfigurations function.
        /// updates app config params.
        /// </summary>
        /// <param name="responseObj">the info came from srv</param>
        private void UpdateConfigurations(CommandRecievedEventArgs responseObj)
        {
            try
            {
                this.OutputDirectory = responseObj.Args[0];
                this.SourceName = responseObj.Args[1];
                this.LogName = responseObj.Args[2];
                this.TumbSize = responseObj.Args[3];
                string[] handlers = responseObj.Args[4].Split(';');
                foreach (string handler in handlers)
                {
                    this.Handlers.Add(handler);
                }
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// CloseHandler function.
        /// </summary>
        /// <param name="responseObj">the info came from srv</param>
        private void CloseHandler(CommandRecievedEventArgs responseObj)
        {
            if (Handlers != null && Handlers.Count > 0 && responseObj != null && responseObj.Args != null
                                 && Handlers.Contains(responseObj.Args[0]))
            {
                this.Handlers.Remove(responseObj.Args[0]);
            }
        }
        /// <summary>
        /// InitializeSettingsParams function.
        /// initializes settings params.
        /// </summary>
        private void InitializeSettingsParams()
        {
            try
            {
                this.OutputDirectory = string.Empty;
                this.SourceName = string.Empty;
                this.LogName = string.Empty;
                this.TumbSize = string.Empty;
                Handlers = new ObservableCollection<string>();
                Object thisLock = new Object();
                BindingOperations.EnableCollectionSynchronization(Handlers, thisLock);
                string[] arr = new string[5];
                CommandRecievedEventArgs request = new CommandRecievedEventArgs((int)CommandEnum.GetConfigCommand, arr, "");
                this.GuiClient.SendCommand(request);
            }
            catch (Exception ex)
            {

            }
        }



        public string OutputDirectory
        {
            get { return m_outputDirectory; }
            set
            {
                m_outputDirectory = value;
                OnPropertyChanged("OutputDirectory");
            }
        }


        public string SourceName
        {
            get { return m_sourceName; }
            set
            {
                m_sourceName = value;
                OnPropertyChanged("SourceName");
            }
        }

        public string LogName
        {
            get { return m_logName; }
            set
            {
                m_logName = value;
                OnPropertyChanged("LogName");
            }
        }

        public string TumbSize
        {
            get { return m_tumbnailSize; }
            set
            {
                m_tumbnailSize = value;
                OnPropertyChanged("TumbnailSize");
            }
        }
        public ObservableCollection<string> Handlers { get; set; }
        #region Notify Changed
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

        #endregion
    }

}