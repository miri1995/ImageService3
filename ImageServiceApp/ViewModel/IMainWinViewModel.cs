using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ImageServiceApp.ViewModel
{
    interface IMainWinViewModel : INotifyPropertyChanged
    {
        ICommand CloseCommand { get; set; }
    }
}
