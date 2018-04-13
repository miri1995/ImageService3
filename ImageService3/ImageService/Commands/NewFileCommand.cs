using ImageService.Modal;
using System;
using System.IO;


namespace ImageService.Commands
{
    /// <summary>
    ///  NewFileCommand class inherits from ICommand.
    /// the command adds a new Image file to the output directory (backup directory).
    /// </summary>
    public class NewFileCommand : ICommand
    {
        #region Members
        private IImageServiceModal m_modal;
        #endregion

        /// <summary>
        /// NewFileCommand constructor.
        /// </summary>
        /// <param name="modal">IImageServiceModal</param>
        public NewFileCommand(IImageServiceModal modal)
        {
            m_modal = modal;
        }

        /// <summary>
        /// <summary>
        /// That function will execute the task according to the command.
        /// </summary>
        /// <param name="args">arguments</param>
        /// <param name="result"> returns true or false according to the success of the command.</param>
        /// <returns>returns a string that contatins a description of the operation the command does.</returns>
        public string Execute(string[] args, out bool result)
        {
            // The String Will Return the New Path if result = true, and will return the error message
            try
            {
                if (File.Exists(args[0]))
                {
                    //add the file to the backup directory.
                    return m_modal.AddFile(args[0], out result);
                }
                else if (args.Length == 0)
                {
                    throw new Exception("No args");
                }

                result = true;
                return string.Empty;

            }
            catch (Exception ex)
            {
                result = false;
                return ex.ToString();
            }
        }
    }
}