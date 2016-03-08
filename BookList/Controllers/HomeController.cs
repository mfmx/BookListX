using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookList.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Read More Books.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Get in touch with us.";

            return View();
        }

        [HttpPost]
        public ActionResult Contact(string message)
        {
            //TODO : send message to HQ
            ViewBag.TheMessage = "Thanks we got your msg.";

            return PartialView("_ContactThanks");
        }
        public ActionResult DashBoard()
        {
            return View();
        }
    }
}