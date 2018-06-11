using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImageServiceWebApp.Models
{
    public class Confirm
    {
        public Confirm(string toDelete)
        {
            Handler = toDelete;
        }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Handler")]
        public string Handler { get; set; }

    }
}