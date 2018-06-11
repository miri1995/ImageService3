using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImageServiceWebApp.Models
{
    public class DeletePhoto
    {

       
        public DeletePhoto(string path, string name, List<Dictionary<string, string>> ListDic)
        {
           
            Thumbnail = path;
            Name = name;
          /*  foreach (Dictionary<string, string> d in ListDic)
            {
                if (name == d["name"])
                {
                    ListDic.Remove(d);
                }

            }*/
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