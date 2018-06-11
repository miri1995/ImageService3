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
        static DeletePhoto deletePoto_model;
        private static string to_delete;



        public HomeController()
        {
            
            config_model = new Config();
            imageWeb_model = new ImageWebInfo();
            // log_model = new Log();
            photo_model = new Photo(config_model.OutputDirectory);
      
            //deletePoto_model=new DeletePhoto();


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

        public ActionResult DeletedPhoto()
        {
            return View();
        }

        public ActionResult DeleteHandler(string toDelete)
        {
            to_delete = toDelete;
            config_model.DeleteHandler(to_delete);
            return View(config_model);
        }

        public ActionResult DeleteView(string path, string name, List<Dictionary<string, string>> ListDic)
        {
            return View(new DeletePhoto(path, name, ListDic));
        }

        public ActionResult ViewPhotos(string path, string name,string year,string month)
        {
            return View(new PhotosV(path, name ,year ,month));
        }

        /*   public ActionResult DeleteH(string toDelete)
           {
               to_delete = toDelete;
               return View(confirm_model);
           }*/



    }
}