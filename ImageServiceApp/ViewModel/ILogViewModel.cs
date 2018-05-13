using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceApp.ViewModel
{
    interface ILogViewModel : INotifyPropertyChanged
    {
        ObservableCollection<LogEntry> VM_LogEntries { get; set; }
    }
}
