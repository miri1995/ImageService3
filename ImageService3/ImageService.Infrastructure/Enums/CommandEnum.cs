using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Infrastructure.Enums
{
    /// <summary>
    /// CommandEnum
    /// </summary>
    public enum CommandEnum : int
    {
        NewFileCommand = 1,
        CloseCommand = 2,
        GetConfigCommand = 3,
        LogCommand = 4,
        CloseHandler = 5,
        AddLogEntry = 6,
        Disconnected = 7

    }
}

