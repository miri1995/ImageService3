using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
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
               
                string orignalPath = s.Replace(pathOfThumb, "Output");
                string name = Path.GetFileName(orignalPath);
           
                string month = Path.GetFileNameWithoutExtension(Path.GetDirectoryName(orignalPath));
                string year = Path.GetFileNameWithoutExtension(Path.GetDirectoryName((Path.GetDirectoryName(orignalPath))));
              //  bool Enabled = false;
                ListDic.Add(new Dictionary<string, string> { { "name", name }, { "Year", year }, { "Month", month }, { "Original", orignalPath }, { "Thumbnail", s } });
               // SpinWait.SpinUntil(() => Enabled);
               //  Enabled = true;
            }

        }


        public void DeleteImage(string m_name,string m_path, string m_month, string m_Thumbnail,string m_year)
        {
            string fullPath = Path.Combine(OutputFolder.Replace("Output", ""), m_path);
            File.Delete(m_Thumbnail);
            File.Delete(fullPath);
          
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





        

    }
}