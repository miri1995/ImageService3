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
        static ImageWebInfo imageWeb_model;
        static Config config_model;
        static Log log_model;
       // static PhotosCollection photo_model;



        public HomeController()
        {
            imageWeb_model = new ImageWebInfo();
            config_model = new Config();
           log_model = new Log();
        //    photo_model = new PhotosCollection();

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

     /*   public ActionResult PhotosViewer()
        {
            return View(photo_model);
        }
        */
    }
}