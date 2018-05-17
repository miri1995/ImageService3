using ImageServiceApp.Communication;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceApp.Model
{
    interface ISettingsModel: INotifyPropertyChanged
    {


        string OutputDirectory { get; set; }
        string SourceName { get; set; }
        string LogName { get; set; }
        string TumbSize { get; set; }
        ObservableCollection<string> Handlers { get; set; }
        IClient GuiClient { get; set; }


    }
}
