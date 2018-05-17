using ImageServiceApp.Communication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceApp.Model
{
    interface IMainWinModel: INotifyPropertyChanged
    {
        bool IsConnected { get; set; }
        IClient GuiClient { get; set; }
    }
}
