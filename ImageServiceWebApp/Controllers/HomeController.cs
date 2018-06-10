using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ImageServiceWebApp.Models;

namespace ImageServiceWebApp.Controllers
{
    public class HomeController : Controller
    {
       
        static Config config_model;
        static Log log_model = new Log();
        static ImageWebInfo imageWeb_model;
         static Photo photo_model;



        public HomeController()
        {
            
            config_model = new Config();
            imageWeb_model = new ImageWebInfo();
            // log_model = new Log();
            photo_model = new Photo(config_model.OutputDirectory);

        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        //GET: ImageWeb
        public ActionResult ImageWeb()
        {
            return View(imageWeb_model);
        }

        public ActionResult Config()
        {
            return View(config_model);
        }

        public ActionResult Logs()
        {
            
            return View(log_model);
        }

       public ActionResult Photos()
        {
            return View(photo_model);
        }
        
    }
}