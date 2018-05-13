using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceApp.Enums
{
    public enum CommandEnum : int
    {
        NotCommand = 0,
        NewFileCommand = 1,
        GetConfigCommand = 2,
        LogCommand = 3,
        CloseCommand = 4
    }
}