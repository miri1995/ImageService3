using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceApp.Model
{
    interface ILogModel
    {
        void AddToList(MessageRecievedEventArgs e);
    }
}
