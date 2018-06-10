﻿using ImageService.Commands;
using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Modal;
using ImageService.Server;
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
        private ImageServer m_imageServer;
        private ILoggingService m_loggingService;

        #endregion

        /// <summary>
        /// ImageController Constructor.
        /// </summary>
        /// <param name="modal">Modal of the system</param>
        public ImageController(IImageServiceModal modal, ILoggingService loggingService)
        {
            m_modal = modal;                    // Storing the Modal Of The System
            m_loggingService = loggingService;
            commands = new Dictionary<int, ICommand>();
            //if (ImageServer == null)
            //{
            //    MessageBox.Show("IMAGE SERVER IS NULL!!");
            //}
            // For Now will contain NEW_FILE_COMMAND
            this.commands[((int)CommandEnum.NewFileCommand)] = new NewFileCommand(this.m_modal);
            this.commands[((int)CommandEnum.GetConfigCommand)] = new GetConfigCommand();
            this.commands[((int)CommandEnum.LogCommand)] = new LogCommand(this.m_loggingService);
            this.commands[((int)CommandEnum.ImageWebCommand)] = new ImageWebCommand(this.m_modal);

        }
        public ImageServer ImageServer
        {
            get
            {
                return m_imageServer;
            }
            set
            {
                this.m_imageServer = value;
                this.commands[((int)CommandEnum.CloseHandler)] = new CloseCommand(m_imageServer);

            }
        }
        /// <summary>
        /// Executing the Command Requet
        /// </summary>
        /// <param name="commandID">Command ID</param>
        /// <param name="args">Arguments for the command</param>
        /// <param name="result">Tells is the command succeeded or not.</param>
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