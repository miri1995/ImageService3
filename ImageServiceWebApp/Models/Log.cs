using ImageService.Infrastructure.Enums;
using ImageServiceWebApp.Communication;
using ImageServiceWebApp.Enum;
using ImageService.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Windows.Data;
using System.ComponentModel;

namespace ImageServiceWebApp.Models
{
    public class Log
    {
        private IImageServiceClient client;
       
        public delegate void NotifyAboutChange();
        public event NotifyAboutChange Notify;
        public event PropertyChangedEventHandler PropertyChanged;


        public Log()
        {
            this.client = ImageServiceClient.Instance;
            this.client.RecieveCommand();
            this.client.UpdateResponse += UpdateResponse;
            this.InitializeLogsParams();

            this.client.UpdateResponse += UpdateResponse;

        }


        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "LogList")]
        public List<LogCollection> LogList { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "stringList")]
        public ObservableCollection<Tuple<string, string>> stringList { get; set; }
       

        private void InitializeLogsParams()
        {
            List<LogCollection> logList = new List<LogCollection>();
            stringList = new ObservableCollection<Tuple<string, string>>();
            BindingOperations.EnableCollectionSynchronization(stringList, new object());
            stringList.CollectionChanged += (s, e)=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("stringList"));
            CommandRecievedEventArgs commandRecievedEventArgs = new CommandRecievedEventArgs((int)CommandEnum.LogCommand, null, "");
            this.client.SendCommand(commandRecievedEventArgs);
        }

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
                  LogList.Add(new LogCollection { LogType = log.Type, Message = log.Message });
                   stringList.Add(new Tuple<string,string>(log.Type, log.Message));
                }
            }
            catch (Exception ex)
            {
                
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
                LogList.Insert(0, new LogCollection { LogType = newLogEntry.Type, Message = newLogEntry.Message });
                stringList.Add( new Tuple<string, string>(responseObj.Args[0], responseObj.Args[1]));
            }
            catch (Exception ex)
            {
              
            }
        }

    }
}