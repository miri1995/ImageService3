using System;


namespace ImageService.Logging.Modal
{
    /// <summary>
    /// MessageRecievedEventArgs class inherits from EventArgs.
    /// responsible on the arguments to the log
    /// </summary>
    public class MessageRecievedEventArgs : EventArgs
    {
        #region members
        //members
        private MessageTypeEnum m_status;
        private string m_message;
        #endregion
        #region Properties
        public MessageTypeEnum Status
        {
            get { return m_status; }
            set { m_status = value; }
        }
        public string Message
        {
            get { return m_message; }
            set { m_message = value; }
        }
        #endregion

        /// <summary>
        /// MessageRecievedEventArgs constructor.
        /// </summary>
        ///<param name="status"> status of the message</param>
        /// <param name="message"> message</param>
        public MessageRecievedEventArgs(MessageTypeEnum status, string message)
        {
            this.m_status = status;
            this.m_message = message;
        }
    }
}