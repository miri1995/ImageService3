using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace ImageServiceWebApp.Models
{
    public class Photo
    {
        
      
        public Photo(string outputFolder)
        {
            OutputFolder = outputFolder;
            ListDic = new List<Dictionary<string, string>>();
            string pathOfThumb = Path.Combine(outputFolder, "Thumbnails");
            string[] listThumbPath = Directory.GetFiles(pathOfThumb, "*", SearchOption.AllDirectories);
            foreach(string s in listThumbPath)
            {
                string orignalPath = s.Replace("Thumbnails", outputFolder);
                string name = Path.GetFileName(orignalPath);
               // DateTime date = GetExplorerFileDate(orignalPath);
               // string datetime = date.ToString("MM:dd:yyyy");
                ListDic.Add(new Dictionary<string, string> { { "name", name }, /*{ "date", datetime },*/ { "Original", orignalPath }, { "Thumbnail", s } });
            }

        }



        static DateTime GetExplorerFileDate(string filename)
        {
            DateTime now = DateTime.Now;
            TimeSpan localOffset = now - now.ToUniversalTime();
            return File.GetLastWriteTimeUtc(filename) + localOffset;
        }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "OutputFolder")]
        public string OutputFolder { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "ThumbnailSize")]
        public string ThumbnailSize { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "ListDic")]
        public List<Dictionary<string,string>> ListDic { get; set; }





        //members
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Year")]
        public string Year { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Month")]
        public string Month { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        [Display(Name = "ImageUrl")]
        public string ImageUrl { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        [Display(Name = "ImageRelativePath")]
        public string ImageRelativePathThumbnail { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        [Display(Name = "ImageRelativePath")]
        public string ImageRelativePath { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        [Display(Name = "ImageRelativePath")]
        public string ImageFullUrl { get; set; }

    }
}