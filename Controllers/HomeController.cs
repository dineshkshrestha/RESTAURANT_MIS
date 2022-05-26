using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RESTAURANT_MIS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            TempData["variable"] = "Welcome to ASP.NET MVC!";
            ViewData["variable"] = "Welcome to ASP.NET MVC!";

            ViewBag.Message = "Welcome to our application.";
            ViewBag.MessageType = "Warning";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}