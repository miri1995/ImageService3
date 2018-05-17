using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceApp.ViewModel
{
    interface ISettingsViewModel
    {
        string OutputDirectory { get; }
        string SourceName { get; }
        string LogName { get; }
        string TumbSize { get; }
        ObservableCollection<string> VM_Handlers { get; }

    }
}
