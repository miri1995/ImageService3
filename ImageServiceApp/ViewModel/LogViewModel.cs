using ImageServiceApp.Model;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ImageServiceApp.ViewModel
{

    class LogViewModel : INotifyPropertyChanged, ILogViewModel
    {
        private LogModel LogsModel;
        public event PropertyChangedEventHandler PropertyChanged;


        public LogViewModel()
        {
            LogsModel = new LogModel();
            LogsModel.PropertyChanged +=
               delegate (Object sender, PropertyChangedEventArgs e) {
                   NotifyPropertyChanged(e.PropertyName);
               };
        }

        public ObservableCollection<MessageRecievedEventArgs> LogMessageList
        {
            get { return LogsModel.LogMessageList; }
        }

      
        public LogModel LogModel
        {
            get { return this.LogsModel; }
            set
            {
                this.LogsModel = value;
            }
        }


        protected void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}