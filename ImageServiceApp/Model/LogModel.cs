using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Data;
using ImageServiceApp.Event;
using ImageServiceApp.Enums;
using System.Windows;
using Newtonsoft.Json;
using ImageServiceApp.Communication;



namespace ImageServiceApp.Model
{
    /// <summary>
    /// Implementation of ILogModel interface.
    /// </summary>
    class LogModel : ILogModel
    {
        // List of all the event log entries.
        private ObservableCollection<LogMessage> logEntries;
        public IClient GuiClient { get; set; }
        // Property - List of all the event log entries. 
        public ObservableCollection<LogMessage> LogEntries
        {
            get
            {
                return this.logEntries;
            }
            set { throw new NotImplementedException(); }
         }

        // boolean property, represents if the model is connected to the image service.
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Log model constructor.
        /// </summary>
        public LogModel()
        {
            this.GuiClient = Client.Instance;
            this.GuiClient.UpdateResponse += UpdateResponse;
            this.InitializeLogsParams();
        }

        /// <summary>
        /// retreive event log entries list from the image service.
        /// </summary>
        private void InitializeLogsParams()
        {
            this.logEntries = new ObservableCollection<LogMessage>();
            Object thisLock = new Object();
            BindingOperations.EnableCollectionSynchronization(LogEntries, thisLock);
            CommandRecievedEventArgs commandRecievedEventArgs = new CommandRecievedEventArgs((int)CommandEnum.LogCommand, null, "");
            this.GuiClient.SendCommand(commandRecievedEventArgs);
        }

        /// <summary>
        /// get CommandRecievedEventArgs object which was sent from the image service.
        /// reacts only if the commandID is relevant to the log model.
        /// </summary>
        /// <param name="responseObj"></param>
        private void UpdateResponse(CommandRecievedEventArgs responseObj)
        {
            if (responseObj != null)
            {
                switch (responseObj.CommandID)
                {
                   
                    case (int)CommandEnum.LogCommand:
                        IntializeLogEntriesList(responseObj);
                        break;
                    case (int)CommandEnum.AddLogEntry:
                        AddLogEntry(responseObj);
                        break;

                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Initialize log event entries list.
        /// </summary>
        /// <param name="responseObj">expected json string of ObservableCollection<LogEntry> in responseObj.Args[0]</param>
        private void IntializeLogEntriesList(CommandRecievedEventArgs responseObj)
        {
            try
            {
                foreach (LogMessage log in JsonConvert.DeserializeObject<ObservableCollection<LogMessage>>(responseObj.Args[0]))
                {
                    this.LogEntries.Add(log);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// adds new log entry to the event log entries list
        /// </summary>
        /// <param name="responseObj">expected responseObj.Args[0] = EntryType,  responseObj.Args[1] = Message </param>
        private void AddLogEntry(CommandRecievedEventArgs responseObj)
        {
            try
            {
                LogMessage newLogEntry = new LogMessage { Type = responseObj.Args[0], Message = responseObj.Args[1] };
                this.LogEntries.Insert(0, newLogEntry);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}