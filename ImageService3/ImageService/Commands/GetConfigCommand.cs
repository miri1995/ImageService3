using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ImageService.Infrastructure.Enums;

using System.Configuration;
using ImageService.Modal;

namespace ImageService.Commands
{
    public class GetConfigCommand:ICommand
    {
        /// <summary>
		/// That function will execute the task of the command.
		/// </summary>
		/// <param name="args">arguments</param>
		/// <param name="result"> tells if the command succeded or not.</param>
		/// <returns>command return a string describes the operartion of the command.</returns>
		public string Execute(string[] args, out bool result)
        {
            try
            {
                result = true;
                string[] arr = new string[5];
                arr[0] = ConfigurationManager.AppSettings.Get("OutputDir");
                arr[1] = ConfigurationManager.AppSettings.Get("SourceName");
                arr[2] = ConfigurationManager.AppSettings.Get("LogName");
                arr[3] = ConfigurationManager.AppSettings.Get("ThumbnailSize");
                arr[4] = ConfigurationManager.AppSettings.Get("Handler");
                CommandRecievedEventArgs commandSendArgs = new CommandRecievedEventArgs((int)CommandEnum.GetConfigCommand, arr, "");
                return JsonConvert.SerializeObject(commandSendArgs);
            }
            catch (Exception ex)
            {
                result = false;
                return ex.ToString();
            }
        }
    }
}