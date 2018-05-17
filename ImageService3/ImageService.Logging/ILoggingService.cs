using ImageService.Logging.Modal;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ImageService.Logging
{
    public delegate void UpdateLogEntry(CommandRecievedEventArgs updateObj);
    /// <summary>
    /// ILoggingService interface.
    /// responsible of the logging writing
    /// </summary>
    public interface ILoggingService
    {
        /// <summary>
        /// MessageRecieved event.
        /// writes messages to the log
        /// </summary>
        event EventHandler<MessageRecievedEventArgs> MessageRecieved;
        
        void Log(string message, MessageTypeEnum type);
        ObservableCollection<LogEntry> LogMessages { get; set; }   //log entries list
        event UpdateLogEntry UpdateLogEntries;  //Invoked everytime a new event log entry is written to the log
        void InvokeUpdateEvent(string message, MessageTypeEnum type); // Invokes UpdateLogEntries event.
    }
}