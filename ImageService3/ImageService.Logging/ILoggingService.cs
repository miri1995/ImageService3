﻿using ImageService.Logging.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Logging
{
    /// <summary>
    /// ILoggingService interface.
    /// manages the logging writting
    /// </summary>
    public interface ILoggingService
    {
        /// <summary>
        /// MessageRecieved event.
        /// in charge of wriiting message to log
        /// </summary>
        event EventHandler<MessageRecievedEventArgs> MessageRecieved;
        /// <summary>
        /// message function.
        /// writes the message to log
        /// </summary>
        /// <param name="message"> the message</param>
        /// <param name="type">type of message</param>
        void Log(string message, MessageTypeEnum type);           // Logging the Message
    }
}