using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImageServiceWebApp.Models
{
    public class PhotosV
    {
        public PhotosV(string path, string name,string year,string month)
        {
            Original = path;
            Name = name;
            Year = year;
            Month = month;
        }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Original")]
        public string Original { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Year")]
        public string Year { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Month")]
        public string Month { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}