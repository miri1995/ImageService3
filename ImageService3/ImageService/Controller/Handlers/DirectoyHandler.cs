using ImageService.Modal;
using System;
using System.IO;
using System.Linq;
using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Logging.Modal;
using ImageService.Server;

namespace ImageService.Controller.Handlers
{
    /// <summary>
    /// Directory Handler class that Implements IDirectoryHandler Interface.
    /// </summary>
    public class DirectoyHandler : IDirectoryHandler
    {
        #region Members
        private IImageController m_controller;              // The Image Processing Controller
        private ILoggingService m_logging;                  // The logger
        private FileSystemWatcher m_dirWatcher;             // The Watcher of the Dir
        private string m_path;                              // The Path of directory
        private readonly string[] suffix = { ".jpg", ".png", ".gif", ".bmp" };   //The file suffixes that are relevant.
        #endregion

        #region Events
        public event EventHandler<DirectoryCloseEventArgs> DirectoryClose;    // The Event That Notifies that the Directory is being closed
        #endregion

        /// <summary>
        /// Constructor of DirectoyHandler
        /// </summary>
        /// <param name="logging">ILoggingService object</param>
        /// <param name="controller">IImageController object</param>
        /// <param name="path">the path of the directory</param>
        public DirectoyHandler(ILoggingService logging, IImageController controller, string path)
        {
            this.m_logging = logging;
            this.m_controller = controller;
            this.m_path = path;
            this.m_dirWatcher = new FileSystemWatcher(this.m_path);
        }

        /// <summary>
        ///  This meothod will be activated when the CommandRecived event will be invoked.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">arguments of CommandRecieved event.</param>
        public void OnCommandRecieved(object sender, CommandRecievedEventArgs e)
        {
            bool result;
            // execute the command
            string msg = this.m_controller.ExecuteCommand(e.CommandID, e.Args, out result);
            // write result msg to the event log.
            if (result)
            {
                this.m_logging.Log(msg, MessageTypeEnum.INFO);
            }
            else
            {
                this.m_logging.Log(msg, MessageTypeEnum.FAIL);
            }
        }

        /// <summary>
        /// The Function handles the given directory
        /// </summary>
        /// <param name="dirPath">Directory path</param
        public void StartHandleDirectory(string dirPath)
        {
            Directory.GetFiles(m_path);
            this.m_dirWatcher.Created += new FileSystemEventHandler(WatcherCreated);
            this.m_dirWatcher.Changed += new FileSystemEventHandler(WatcherCreated);
            //listen to directory
            this.m_dirWatcher.EnableRaisingEvents = true;
            this.m_logging.Log("Start handle directory: " + dirPath, MessageTypeEnum.INFO);
        }

        /// <summary>
        /// when there are changes of the files in the directory (a file is being added, a file is being changes
        /// this method is activated.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">Arguments of FileSystemEvent.</param>
        private void WatcherCreated(object sender, FileSystemEventArgs e)
        {
            this.m_logging.Log("In Watcher Created with: " + e.FullPath, MessageTypeEnum.INFO);
            string thisSuffix = Path.GetExtension(e.FullPath);
            // check that the file is an image.
            if (this.suffix.Contains(thisSuffix))
            {
                string[] args = { e.FullPath };
                CommandRecievedEventArgs commandRecievedEventArgs =
                    new CommandRecievedEventArgs((int)CommandEnum.NewFileCommand, args, "");
                this.OnCommandRecieved(this, commandRecievedEventArgs);
            }


        }

        /// <summary>
        ///  This meothod will be activated when the stop server event will be invoked- when the 
        ///  server is closed.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">arguments of DirectoryClose event.</param>
        public void CloseHandler(object sender, DirectoryCloseEventArgs e)
        {
            try
            {
                // stop listen to directory.
                this.m_dirWatcher.EnableRaisingEvents = false;
                // remove OnCommandRecieved from the CommandRecived Event.
                ((ImageServer)sender).CommandRecieved -= this.OnCommandRecieved;
                this.m_logging.Log("Succsess close handler of path " + this.m_path, MessageTypeEnum.INFO);
            }
            catch (Exception exception)
            {
                this.m_logging.Log("Error close handler of path " + this.m_path + " " + exception.ToString(), MessageTypeEnum.FAIL);
            }
        }

    }
}