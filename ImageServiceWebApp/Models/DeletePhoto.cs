using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImageServiceWebApp.Models
{
    public class DeletePhoto
    {

       
        public DeletePhoto(string path, string name)
        {
           
            Thumbnail = path;
            Name = name;
        }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Thumbnail")]
        public string Thumbnail { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string Name { get; set; }

       
    }
}