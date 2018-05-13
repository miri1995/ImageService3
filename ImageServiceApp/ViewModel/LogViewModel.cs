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

    class LogViewModel : ILogViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ILogModel logModel = new LogModel();
       
        //list of all the logs.
        public ObservableCollection<LogEntry> VM_LogEntries
        {
            get { return this.logModel.LogEntries; }
            set => throw new NotImplementedException();
            }
        }
    }
}
