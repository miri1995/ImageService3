using ImageService.Commands;
using ImageService.Infrastructure.Enums;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImageService.Controller
{
    /// <summary>
    /// ImageController. Implements the IImageController Interface.
    /// </summary>
    public class ImageController : IImageController
    {
        #region Members
        private IImageServiceModal m_modal;                      // The Modal Object
        private Dictionary<int, ICommand> commands;             // Commands dictionary : [Command ID, Command]
        #endregion

        /// <summary>
        /// ImageController Constructor.
        /// </summary>
        /// <param name="modal">Modal of the system</param>
        public ImageController(IImageServiceModal modal)
        {
            m_modal = modal;                    // Storing the Modal Of The System
            commands = new Dictionary<int, ICommand>();


            this.commands[((int)CommandEnum.NewFileCommand)] = new NewFileCommand(this.m_modal);
        }

        /// <summary>
        /// the function executes the command
        /// </summary>
        /// <param name="commandID">Command ID</param>
        /// <param name="args">Arguments for the command</param>
        /// <param name="result">the result is true if the command succeeded.</param>
        /// <returns></returns>
        public string ExecuteCommand(int commandID, string[] args, out bool resultSuccesful)
        {
            Task<Tuple<string, bool>> task = new Task<Tuple<string, bool>>(() => {
                bool resultSuccesfulTemp;
                string message = this.commands[commandID].Execute(args, out resultSuccesfulTemp);
                return Tuple.Create(message, resultSuccesfulTemp);
            });
            task.Start();
            task.Wait();
            Tuple<string, bool> result = task.Result;
            resultSuccesful = result.Item2;
            return result.Item1;
        }
    }
}