using ImageService.Modal;
using System;


namespace ImageService.Controller.Handlers
{
    /// <summary>
    /// Interface which listens to a certain directory and does operations on it according
    /// to the changes being made
    /// </summary>
    public interface IDirectoryHandler
    {
        /// The Event That Notifies that the Directory is being closed
        event EventHandler<DirectoryCloseEventArgs> DirectoryClose;

        /// <summary>
        /// The Function handles the given directory
        /// </summary>
        /// <param name="dirPath">Directory path</param>
        void StartHandleDirectory(string dirPath);

        /// <summary>
        ///  This meothod will be activated when the CommandRecived event will be invoked.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">arguments of CommandRecieved event.</param>
        void OnCommandRecieved(object sender, CommandRecievedEventArgs e);

        /// <summary>
        ///  This meothod will be activated when the stop server event will be invoked- when the 
        ///  server is closed.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">arguments of DirectoryClose event.</param>
        void CloseHandler(object sender, DirectoryCloseEventArgs e);
    }
}