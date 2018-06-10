using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImageServiceWebApp.Models
{
    public class PhotosV
    {
        public PhotosV(string path, string name/*string date*/)
        {
            Original = path;
            Name = name;
          //  Date = date;
        }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Original")]
        public string Original { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Date")]
        public string Date { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}