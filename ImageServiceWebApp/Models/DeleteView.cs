using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImageServiceWebApp.Models
{
    public class DeleteView
    {

       
        public DeleteView(string path, string name, Dictionary<string, string> dict)
        {
           
            Thumbnail = path;
            Name = name;
            Dict = dict;
          
        }

       /* private void DeleteImage()
        {

        }*/

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Thumbnail")]
        public string Thumbnail { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Dict")]
        public Dictionary<string, string> Dict { get; set; }


    }
}