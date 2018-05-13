using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using ImageServiceApp;
using ImageServiceApp.Model;

namespace ImageServiceApp.Models
{
    class LogModel : INotifyPropertyChanged, ILogModel
    {
        // an event that raises when a property is being changed
        public event PropertyChangedEventHandler PropertyChanged;

        public LogModel()
        {
            this.m_LogMessageList = new ObservableCollection<MessageRecievedEventArgs>();

            this.m_LogMessageList.Add(new MessageRecievedEventArgs() { Status = MessageTypeEnum.INFO, Message = "Test Message" });
            this.m_LogMessageList.Add(new MessageRecievedEventArgs() { Status = MessageTypeEnum.WARNING, Message = "Test Message2" });
            this.m_LogMessageList.Add(new MessageRecievedEventArgs() { Status = MessageTypeEnum.FAIL, Message = "Test Message3" });
        }



        protected void OnPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private ObservableCollection<MessageRecievedEventArgs> m_LogMessageList;
        public ObservableCollection<MessageRecievedEventArgs> LogMessageList
        { //get; set;
            get { return this.m_LogMessageList; }
            set
            {
                this.m_LogMessageList = value;
                OnPropertyChanged("LogMessageList");
            }
        }

        public void AddToList(MessageRecievedEventArgs e)
        {
            this.m_LogMessageList.Add(e);
            OnPropertyChanged("AddToList");
        }

       
    }
}
