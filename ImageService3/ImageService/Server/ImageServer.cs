using ImageService.Controller;
using ImageService.Controller.Handlers;
using ImageService.Logging;
using ImageService.Modal;
using System;
using System.Configuration;


namespace ImageService.Server
{
    public class ImageServer
    {
        /// <summary>
        /// ImageServer class.
        /// </summary>
        #region Members
        private IImageController m_controller;
        private ILoggingService m_logging;
        #endregion

        #region Properties
        public event EventHandler<CommandRecievedEventArgs> CommandRecieved;          // The event that notifies about a new Command being recieved
        public event EventHandler<DirectoryCloseEventArgs> CloseServer;
        #endregion


        /// <summary>
        /// ImageServer constructor.
        /// </summary>
        /// <param name="controller">IImageController object/param>
        /// <param name="logging">ILoggingService object</param
        public ImageServer(IImageController controller, ILoggingService logging)
        {
            this.m_controller = controller;
            this.m_logging = logging;
            string[] directories = (ConfigurationManager.AppSettings.Get("Handler").Split(';'));

            foreach (string path in directories)
            {
                try
                {
                    this.CreateHandler(path);
                }
                catch (Exception ex)
                {
                    this.m_logging.Log("Error create handler for directory: " + path + " because:" + ex.ToString(), Logging.Modal.MessageTypeEnum.FAIL);
                }
            }
        }
        /// <summary>
        /// function that creates the handler.
        /// </summary>
        /// <param name="path">the path of the directory the handler is in charge on</param>
        private void CreateHandler(string path)
        {
            IDirectoryHandler handler = new DirectoyHandler(m_logging, m_controller, path);
            CommandRecieved += handler.OnCommandRecieved;
            this.CloseServer += handler.CloseHandler;
            handler.StartHandleDirectory(path);
            this.m_logging.Log("Created handler for directory: " + path, Logging.Modal.MessageTypeEnum.INFO);
        }
        /// <summary>
        /// the function that closes the server.
        /// </summary>
        public void StopServer()
        {
            try
            {
                m_logging.Log("Stop Server", Logging.Modal.MessageTypeEnum.INFO);
                CloseServer?.Invoke(this, null);
               
                //  m_logging.Log("Leave OnCloseServer", Logging.Modal.MessageTypeEnum.INFO);
            }
            catch (Exception ex)
            {
                this.m_logging.Log("Stop Server Exception: " + ex.ToString(), Logging.Modal.MessageTypeEnum.FAIL);
            }
        }
    }
}