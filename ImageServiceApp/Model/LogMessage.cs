using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageServiceApp;

namespace ImageServiceApp.Model
{
    class LogMessage : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private string m_Message;
        public string Message
        {
            get { return m_Message; }
            set
            {
                m_Message = value;
                OnPropertyChanged("Message");
            }
        }

        private string m_MessageType;
        //private MessageTypeEnum m_MessageTypeEnum;
        public string Type
        {
            get
            {
                return this.m_MessageType;

            }

            set
            {
                this.m_MessageType = value;
                OnPropertyChanged("Type");

            }
        }

        public string ColorMatch()
        {
            switch (this.m_MessageType)
            {
                case "FAIL":
                    return "Red";
                case "WARNING":
                    return "Yellow";
                case "INFO":
                    return "Green";
                default:
                    return "Transparent";
            }

        }



        public LogMessage(string MessageType, string Message)
        {
            this.m_MessageType = MessageType;
            this.m_Message = Message;
        }
    }
}
