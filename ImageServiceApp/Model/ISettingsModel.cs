using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceApp.Model
{
    interface ISettingsModel
    {
      

        string OutputDirectory { get; set; }
        string SourceName { get; set; }
        string LogName { get; set; }
        string ThumbSize { get; set; }
        string ChosenHandler { get; set; }
        ObservableCollection<string> Directories { get; set; }
        void sendToServer();
    }
}
