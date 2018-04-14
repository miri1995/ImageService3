using ImageService.Logging.Modal;
using System;


namespace ImageService.Logging
{
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
        /// <summary>
        /// the function is responsible of the messages.
        /// </summary>
        /// <param name="message"> the message written to the log</param>
        /// <param name="type">the type of message</param>
        void Log(string message, MessageTypeEnum type);           // Logging the Message
    }
}