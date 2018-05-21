using ImageServiceApp.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace ImageServiceApp.Model
{
    interface ILogModel: INotifyPropertyChanged
    {
        ObservableCollection<LogMessage> LogEntries { get; set; }
    }
}
