using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageServiceApp.Event;

namespace ImageServiceApp.Communication
{
    public delegate void UpdateResponseArrived(CommandRecievedEventArgs responseObj);

    interface IClient
    {
        
        void SendCommand(CommandRecievedEventArgs commandRecievedEventArgs);
        
        
        
        void RecieveCommand();
        event UpdateResponseArrived UpdateResponse;
        bool Connected { get; set; }
        void Disconnected();


    }
}
