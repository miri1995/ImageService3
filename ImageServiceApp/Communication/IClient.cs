using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageServiceApp.Event;

namespace ImageServiceApp.Communication
{
    interface IClient
    {
        void start();
        void Sender(object sender, CommandRecievedEventArgs e);
        void Disconnect();
        


    }
}
