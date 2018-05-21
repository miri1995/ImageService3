using ImageService.Infrastructure.Enums;
using ImageService.Logging.Modal;
using ImageService.Modal;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ImageService.Logging
{
    /// <summary>
    /// LoggingService class implements ILoggingService interface.
    /// responsible of the logging writing
    /// </summary>
    public class LoggingService : ILoggingService
    {

        /// <summary>
        /// MessageRecieved event.
        /// in charge of wriiting message to log
        /// </summary>
        public event EventHandler<MessageRecievedEventArgs> MessageRecieved;

        //Invoked everytime a new event log entry is written to the log
        public event UpdateLogEntry UpdateLogEntries;

        // list of all the event log entries.
        private ObservableCollection<LogMessage> logMessages;

        // property that wrapps logMessages.
        public ObservableCollection<LogMessage> LogMessages 
        {
            get { return this.logMessages; }
            set { throw new NotImplementedException(); }
        }

        /// <summary>
        /// LoggingService constructor.
        /// </summary>
        /// <param name="eventLog">EventLog of the ImageService</param>
        public LoggingService(EventLog eventLog)
        {
            this.logMessages = new ObservableCollection<LogMessage>();
            // retrieve all the eventLog entries and save them in this.logMessages.
            GetAllLogEventMessages(eventLog);
        }

        /// <summary>
        /// message function.
        /// writes the message to log
        /// </summary>
        /// <param name="message"> the message</param>
        /// <param name="type">type of message</param>
        ///
        public void Log(string message, MessageTypeEnum type)
        {
            MessageRecieved?.Invoke(this, new MessageRecievedEventArgs(type, message));

            // adds the new event log entry to logMessages.
            LogMessage newLogEnrty = new LogMessage { Type = Enum.GetName(typeof(MessageTypeEnum), type), Message = message };
            this.LogMessages.Insert(0, newLogEnrty);

            InvokeUpdateEvent(message, type);
        }

        /// <summary>
        /// Invokes UpdateLogEntries event.
        /// </summary>
        /// <param name="message"> message</param>
        /// <param name="type">entry type</param>
        public void InvokeUpdateEvent(string message, MessageTypeEnum type)
        {
            LogMessage newLogEnrty = new LogMessage { Type = Enum.GetName(typeof(MessageTypeEnum), type), Message = message };
            string[] args = new string[2];

            // args[0] = EntryType, args[1] = Message
            args[0] = newLogEnrty.Type;
            args[1] = newLogEnrty.Message;
            CommandRecievedEventArgs updateObj = new CommandRecievedEventArgs((int)CommandEnum.AddLogEntry, args, null);
            if (this.UpdateLogEntries != null)
            {
                UpdateLogEntries?.Invoke(updateObj);

            }
        }

        /// <summary>
        /// retrieve all the image service event log entries and adds 
        /// </summary>
        /// <param name="eventLog"></param>
        private void GetAllLogEventMessages(EventLog eventLog)
        {
            eventLog.WriteEntry("Enter GetAllLogEventMessages", EventLogEntryType.Warning);
            EventLogEntry[] logs = new EventLogEntry[eventLog.Entries.Count];
            eventLog.Entries.CopyTo(logs, 0);
            foreach (EventLogEntry entry in logs)
            {
                this.LogMessages.Insert(0, new LogMessage
                {
                    Type = Enum.GetName(typeof(MessageTypeEnum), LoggingService.FromLogEventTypeToMessageTypeEnum(entry.EntryType)),
                    Message = entry.Message
                });
            }
        }

        /// <summary>
        /// convert LogEventType to EventLogEntryType.
        /// </summary>
        /// <param name="type">entry type</param>
        /// <returns>equivalent MessageTypeEnum</returns>
        public static MessageTypeEnum FromLogEventTypeToMessageTypeEnum(EventLogEntryType type)
        {
            switch (type)
            {
                case EventLogEntryType.Information:
                    return MessageTypeEnum.INFO;
                case EventLogEntryType.Warning:
                    return MessageTypeEnum.WARNING;
                case EventLogEntryType.Error:
                default:
                    return MessageTypeEnum.FAIL;
            }
        }

        /// <summary>
        /// convert EventLogEntryType to LogEventType.
        /// </summary>
        /// <param name="type">entry type</param>
        /// <returns>equivalent EventLogEntryType</returns>
        public static EventLogEntryType FromMessageTypeEnumToEventLogEntryType(MessageTypeEnum type)
        {
            switch (type)
            {
                case MessageTypeEnum.FAIL:
                    return EventLogEntryType.Error;
                case MessageTypeEnum.WARNING:
                    return EventLogEntryType.Warning;
                case MessageTypeEnum.INFO:
                default:
                    return EventLogEntryType.Information;
            }
        }
    }

}