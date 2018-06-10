using ImageService.Infrastructure.Enums;

using ImageServiceWebApp.Communication;
using ImageServiceWebApp.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using static ImageServiceWebApp.Models.Config;

namespace ImageServiceWebApp.Models
{


    public class ImageWebInfo
    {
        private IImageServiceClient client;
        public event NotifyAboutChange Notify;

        public ImageWebInfo() {
            Counter = "";

           
            this.client = ImageServiceClient.Instance;
            this.client.RecieveCommand();
            this.client.UpdateResponse += UpdateResponse;
            Students = getList();
            string[] arr = new string[1];
            CommandRecievedEventArgs request = new CommandRecievedEventArgs((int)CommandEnum.ImageWebCommand, arr, "");
            this.client.SendCommand(request);

           

        }

     

        private void UpdateResponse(CommandRecievedEventArgs responseObj)
        {
            try
            {
                if (responseObj != null)
                {
                    switch (responseObj.CommandID)
                    {
                        case (int)ImageService.Infrastructure.Enums.CommandEnum.ImageWebCommand:
                            UpdateConfigurations(responseObj);
                            break;

                    }
                    //update controller
                    Notify?.Invoke();
                }
            }
            catch (Exception ex)
            {
                
            }

        }

        private void UpdateConfigurations(CommandRecievedEventArgs responseObj)
        {
            try
            {
                Counter = responseObj.Args[0];
              
            }
            catch (Exception ex)
            {

            }
        }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Status")]
        public string Status { get; set; }


        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Counter")]
        public string Counter { get; set; }


        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Students")]
        public List<Student> Students { get; set; }

        public static List<Student> getList() {

          
                List<Student> list = new List<Student>();
                StreamReader stream = new StreamReader(HttpContext.Current.Server.MapPath("~/App_Data/StudentsDetails.txt"));
                string line = stream.ReadLine();
                while (line != null)
                {
                    string[] token = line.Split(',');
                    list.Add(new Student(token[0], token[1], token[2]));
                    line = stream.ReadLine();
                }
                stream.Close();
                return list;
    }


        public class Student
        {
            private string firstName;
            private string lastName;
            private string id;

            public Student(string firstName, string lastName, string id)
            {
                this.firstName = firstName;
                this.lastName = lastName;
                this.id = id;
            }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "First Name")]
            public string FirstName {
                get { return this.firstName; }
                set { this.firstName = value; }
            }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Last Name")]
            public string LastName
            {
                get { return this.lastName; }
                set { this.lastName = value; }
            }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "ID")]
            public string ID
            {
                get { return this.id; }
                set { this.id = value; }
            }

        }
    }
}