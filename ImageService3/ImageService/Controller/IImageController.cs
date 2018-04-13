

namespace ImageService.Controller
{
    /// <summary>
    /// Interface of ImagController.
    /// </summary>
    public interface IImageController
    {
        /// <summary>
        /// the function executes the command
        /// </summary>
        /// <param name="commandID">Command ID</param>
        /// <param name="args">Arguments for the command</param>
        /// <param name="result">the result is true if the command succeeded.</param>
        /// <returns></returns>
        string ExecuteCommand(int commandID, string[] args, out bool result);
    }
}