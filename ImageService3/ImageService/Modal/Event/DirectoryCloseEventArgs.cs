using System;


namespace ImageService.Modal
{
    /// <summary>
    ///DirectoryCloseEventArgs class that inherits from EventArgs.
    /// </summary>
    public class DirectoryCloseEventArgs : EventArgs
    {
        #region Members
        public string DirectoryPath { get; set; }       // Path of the directory to be closed.
        public string Message { get; set; }             // The Message That goes to the logger.
        #endregion

        /// <summary>
        /// DirectoryCloseEventArgs constructor.
        /// </summary>
        /// <param name="dirPath">Path of the directory we want to close.</param>
        /// <param name="message">that message that is being sent to th logger.</param>
        public DirectoryCloseEventArgs(string dirPath, string message)
        {
            DirectoryPath = dirPath;                    // Setting the Directory Name
            Message = message;                          // Storing the String
        }

    }
}