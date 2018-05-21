
using ImageService.Logging;
using System.Collections.Generic;
using Newtonsoft.Json;
using ImageService.Infrastructure.Enums;
using System.Collections.ObjectModel;
using ImageService.Modal;
using System;
using System.Windows;

namespace ImageService.Commands
{
    public class LogCommand:ICommand
    {
        private ILoggingService loggingService;

        /// <summary>
        /// LogCommand constructor.
        /// </summary>
        /// <param name="loggingService">Image service logger.</param>
        public LogCommand(ILoggingService loggingService)
        {
            this.loggingService = loggingService;
        }
        public string Execute(string[] args, out bool result)
        {
            try
            {
                ObservableCollection<LogMessage> logMessages = this.loggingService.LogMessages;
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
                MessageBox.Show(e.ToString());
                return "LogCommand.Execute: Failed execute log command";
            }
        }
    }
}