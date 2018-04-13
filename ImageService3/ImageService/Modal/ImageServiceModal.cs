using System;
using System.Drawing;
using System.IO;


namespace ImageService.Modal
{
    /// <summary>
    /// ImageServiceModal class which implements the IImageServiceModal Interface.
    /// </summary>
    public class ImageServiceModal : IImageServiceModal
    {
        #region Members
        private string m_OutputFolder;            // The Output Folder
        private int m_thumbnailSize;              // The Size Of The Thumbnail Size
        #endregion

        #region Properties

        public string OutputFolder
        {
            get { return this.m_OutputFolder; }
            set { this.m_OutputFolder = value; }
        }


        public int ThumbnailSize
        {
            get { return this.m_thumbnailSize; }
            set { this.m_thumbnailSize = value; }
        }
        #endregion

        /// <summary>
        /// The Function Addes A file to the system
        /// </summary>
        /// <param name="path">the path of the file/param>
        ///  <param name="result">the result is true or false/param>
        /// <returns>a string if suceeded adding</returns>
        public string AddFile(string path, out bool result)
        {
            try
            {
                string message;
                result = true;
                if (File.Exists(path))
                {
                    // Get the date of the creation of the image
                    DateTime date = GetExplorerFileDate(path);
                    int year = date.Year;
                    int month = date.Month;
                    DirectoryInfo dirOutput = Directory.CreateDirectory(m_OutputFolder);
                    // add to a hidden directory
                    dirOutput.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                    // Create the suitables folders for the file according to it's creation time
                    Directory.CreateDirectory(m_OutputFolder + "\\" + "Thumbnails");
                    this.CreateFolders(m_OutputFolder, year.ToString(), month.ToString());
                    this.CreateFolders(m_OutputFolder + "\\" + "Thumbnails", year.ToString(), month.ToString());
                    string pathTargetFolder = m_OutputFolder + "\\" + year + "\\" + month + "\\";
                    string newPath = pathTargetFolder + Path.GetFileName(path);

                    if (File.Exists(newPath))
                    {
                        newPath = this.GetAvailablePath(newPath, pathTargetFolder);
                    }
                    File.Move(path, newPath);
                    message = "Added " + Path.GetFileName(newPath) + " to " + pathTargetFolder;
                    message = CreateTumbImage(newPath, path, message, year.ToString(), month.ToString());
                    return message;

                }
                else
                {
                    throw new Exception("File doesn't exists");
                }

            }
            catch (Exception exception)
            {
                result = false;
                return exception.ToString();
            }
        }

        static DateTime GetExplorerFileDate(string filename)
        {
            DateTime now = DateTime.Now;
            TimeSpan localOffset = now - now.ToUniversalTime();
            return File.GetLastWriteTimeUtc(filename) + localOffset;
        }


        /// <summary>
        /// The Function returns the new path to the file
        /// </summary>
        /// <param name="newPath">the new path of the file/param>
        ///  <param name="pathTargetFolder">the path of the target folder/param>
        /// <returns>a string of the new path</returns>
        private string GetAvailablePath(string newPath, string pathTargetFolder)
        {
            int i = 1;
            string fileName = Path.GetFileNameWithoutExtension(newPath);
            string extension = Path.GetExtension(newPath);
            while (File.Exists((newPath = pathTargetFolder + fileName + " (" + i.ToString() + ")" + extension)))
            {
                i++;
            }
            return newPath;
        }

        /// <summary>
        /// creates a folder for the year and inside this folder creates a folder for the month .
        /// </summary>
        /// <param name="dirPath">Directory path.</param>
        /// <param name="year">year</param>
        /// <param name="month">month</param>
        private void CreateFolders(string dirPath, string year, string month)
        {
            Directory.CreateDirectory(dirPath + "\\" + year);
            Directory.CreateDirectory(dirPath + "\\" + year + "\\" + month);
        }


        /// <summary>
        /// creates the image in thumbnailSize.
        /// </summary>
        /// <param name="newPath">new path</param>
        /// <param name="path">old path</param>
        /// <param name="message">a message of adding the thumb file</param>
        /// <param name="year">year</param>
        /// <param name="month">month</param>
        /// <returns>a the message of adding the thumb file </returns>
        private string CreateTumbImage(string newPath, string path, string message, string year, string month)
        {
            // create a thumb photo
            string thumbsNewPath = m_OutputFolder + "\\" + "Thumbnails" + "\\" + year + "\\" + month + "\\" + Path.GetFileName(path);
            if (File.Exists(thumbsNewPath))
            {
                thumbsNewPath = this.GetAvailablePath(thumbsNewPath, m_OutputFolder + "\\" + "Thumbnails" + "\\" + year + "\\" + month + "\\");
            }

            Image thumb = Image.FromFile(newPath);
            thumb = (Image)(new Bitmap(thumb, new Size(this.m_thumbnailSize, this.m_thumbnailSize)));
            thumb.Save(thumbsNewPath);
            message += " and added thumb " + Path.GetFileName(path);
            return message;
        }
    }
}