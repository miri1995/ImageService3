

using ImageServiceWebApp.Enum;
using ImageServiceWebApp.Communication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ImageService.Logging;
using ImageService.Infrastructure.Enums;

namespace ImageServiceWebApp.Models
{
    public class LogCollection
    {
  
  
        public enum EntryType
        {
            INFO,
            FAIL,
            WARNING
        }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "LogType")]
        public string LogType { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Message")]
        public string Message { get; set; }

    }
}