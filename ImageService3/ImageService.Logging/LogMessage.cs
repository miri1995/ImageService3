using ImageService.Logging.Modal;
using System;


namespace ImageService.Logging
{
    public class LogMessage
    {
        private MessageTypeEnum type;
        public string Type
        {
            get { return Enum.GetName(typeof(MessageTypeEnum), type); }
            set { this.type = (MessageTypeEnum)Enum.Parse(typeof(MessageTypeEnum), value); }
        }
        public string Message { get; set; }

    }
}
