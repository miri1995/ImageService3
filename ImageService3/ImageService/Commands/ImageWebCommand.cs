using ImageService.Infrastructure.Enums;
using ImageService.Modal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands
{
    public class ImageWebCommand : ICommand
    {
        private IImageServiceModal model;
        public ImageWebCommand(IImageServiceModal model)
        {
            this.model = model;
        }

        public string Execute(string[] args, out bool result)
        {
            result = true;
            string[] arr = new string[1];
            arr[0]= this.model.CounterImages();
            CommandRecievedEventArgs commandSendArgs = new CommandRecievedEventArgs((int)CommandEnum.ImageWebCommand, arr, "");
            return JsonConvert.SerializeObject(commandSendArgs);
            

        }
    }
   
}
