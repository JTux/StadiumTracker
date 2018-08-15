using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StadiumTracker.WebMVC.Controllers
{
    public class ParkController : Controller
    {
        // GET: Park
        public ActionResult Index()
        {
            return View();
        }
    }
}