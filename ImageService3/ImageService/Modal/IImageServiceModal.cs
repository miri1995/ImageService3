

namespace ImageService.Modal
{
    /// <summary>
    /// Image Service Modal Interface.
    /// </summary>
    public interface IImageServiceModal
    {
        /// <summary>
        /// The Function Addes A file to the system
        /// </summary>
        /// <param name="path">the path of the file/param>
        ///  <param name="result">the result is true or false/param>
        /// <returns>a string if suceeded adding</returns>
        string AddFile(string path, out bool result);
      //  string getCounter(out bool result);
        string CounterImages();
         string Status { get; set; }
        
    }
}