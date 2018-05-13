using ImageService.Commands;
using ImageService.Logging;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService3.ImageService.Commands
{
    class LogCommand:ICommand
    {
        private ILoggingService loggingService;


        /// <summary>
        /// LogCommand constructor
        /// </summary>
        /// <param name="loggingService"></param>
        public LogCommand(ILoggingService loggingService)
        {
            this.loggingService = loggingService;
        }



        public string Execute(string[] args, out bool result)
        {
            try
            {
                ObservableCollection<LogEntry> logMessages = this.loggingService.LogMessages;
                // serialize the log entries list to json string.
                string jsonLogMessages = JsonConvert.SerializeObject(logMessages);
                string[] arr = new string[1];
                arr[0] = jsonLogMessages;
                CommandRecievedEventArgs commandSendArgs = new CommandRecievedEventArgs((int)CommandEnum.LogCommand, arr, "");
                result = true;
                // serialize the commandSendArgs to json string.
                return JsonConvert.SerializeObject(commandSendArgs);
            }
            catch (Exception e)
            {
                result = false;
                return "LogCommand.Execute: Failed execute log command";
            }
        }
    }
}
