using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ImageServiceWebApp.Models;
using System.Collections.ObjectModel;
using System.Threading;

namespace ImageServiceWebApp.Controllers
{
    public class HomeController : Controller
    {

        static Config config_model;
        static Log log_model = new Log();
        static ImageWebInfo imageWeb_model;
        static Photo photo_model;
       // static DeleteView deletePoto_model;
        static DeleteView deleteView_model;
        private static string m_toDelete;
        private static Dictionary<string, string>dict;
        private static string m_path;
        private static string m_name;
        private static string m_month;
        private static string m_year;
        private static string m_Thumbnail;



        public HomeController()
        {

            config_model = new Config();
            config_model.Notify += Notify;
            imageWeb_model = new ImageWebInfo();
            // log_model = new Log();
            photo_model = new Photo(config_model.OutputDirectory);
            deleteView_model = new DeleteView(m_path, m_name, dict);

            //deletePoto_model=new DeletePhoto();


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

        
        public ActionResult DeleteHandler(string toDelete)
        {
            m_toDelete = toDelete;
            return RedirectToAction("Confirm");
        }

        public ActionResult Confirm()
        {
            return View(config_model);
        }

        public ActionResult Deleted()
        {
            //delete the handler
            config_model.DeleteHandler(m_toDelete);
            Thread.Sleep(1000);
            return RedirectToAction("Config");

        }
        
        public ActionResult DeleteView(string path,string name,string year,string month, string Thumbnail)
        {
            
            m_name = name;
            m_path = path;
            m_month = month;
            m_Thumbnail = Thumbnail;
            m_year = year;
            return RedirectToAction("Erase");
        }

        public ActionResult Erase()
        {
            return View(deleteView_model);
        }

        public ActionResult DeletedPhoto()
        {
            photo_model.DeleteImage(m_name, m_path, m_month, m_Thumbnail, m_year);
            Thread.Sleep(1000);
            return RedirectToAction("Photos");
        }

        


        public ActionResult ViewPhotos(string path, string name, string year, string month)
        {
            return View(new PhotosV(path, name, year, month));
        }

        public void Notify()
        {
            Config();
        }



    }
}