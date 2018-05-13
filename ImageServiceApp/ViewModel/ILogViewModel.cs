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
    interface ILogViewModel : INotifyPropertyChanged
    {
        ObservableCollection<MessageRecievedEventArgs> LogMessageList { get;}
        LogModel LogModel { get; set; }
    }
}
