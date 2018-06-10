using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImageServiceWebApp.Models
{
    public class Confirm
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "toDelete")]
        public string toDelete { get; set; }

    }
}