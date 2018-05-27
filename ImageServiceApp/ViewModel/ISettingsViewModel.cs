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
        string VM_OutputDirectory { get; }
        string VM_SourceName { get; }
        string VM_LogName { get; }
        string VM_TumbnailSize { get; }
        ObservableCollection<string> VM_Handlers { get; }

    }
}
