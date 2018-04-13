
namespace ImageService.Commands
{
    /// <summary>
    /// Command Interface. 
    /// an object that performs one task.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// That function will execute the task according to the command.
        /// </summary>
        /// <param name="args">arguments</param>
        /// <param name="result"> returns true or false according to the success of the command.</param>
        /// <returns>returns a string that contatins a description of the operation the command does.</returns>
        string Execute(string[] args, out bool result);
    }
}