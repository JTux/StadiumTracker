using System.Web.Mvc;

namespace StadiumTracker.WebMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}