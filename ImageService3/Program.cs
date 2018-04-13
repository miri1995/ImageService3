using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ImageService3
{
    static class Program
    {
        static void Main(string[] args)
        {
            ServiceBase[] ServicesToRun = new ServiceBase[] { new ImageService3(args) };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
