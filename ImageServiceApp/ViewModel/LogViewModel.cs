using ImageServiceApp.Enums;
using ImageServiceApp.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;




namespace ImageServiceApp.ViewModel
{

   public class LogViewModel : ILogViewModel
    {
        private ILogModel logModel = new LogModel();

        public event PropertyChangedEventHandler PropertyChanged;
       

        // List of all the event log entries.
        public ObservableCollection<LogMessage> LogEntries
        {
            get { return this.logModel.LogEntries; }
            set { throw new NotImplementedException(); }
            
        }
    }
}
