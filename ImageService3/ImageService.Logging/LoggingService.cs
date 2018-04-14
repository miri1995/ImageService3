using ImageService.Logging.Modal;
using System;


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
        /// writes messages to the log
        /// </summary>
        public event EventHandler<MessageRecievedEventArgs> MessageRecieved;
        /// <summary>
        /// the function is responsible of the messages.
        /// </summary>
        /// <param name="message"> the message written to the log</param>
        /// <param name="type">the type of message</param>
        public void Log(string message, MessageTypeEnum type)
        {
            MessageRecieved?.Invoke(this, new MessageRecievedEventArgs(type, message));

        }
    }

}