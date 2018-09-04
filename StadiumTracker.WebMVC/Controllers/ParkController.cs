using Microsoft.AspNet.Identity;
using StadiumTracker.Models.ParkModels;
using StadiumTracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StadiumTracker.WebMVC.Controllers
{
    [Authorize]
    public class ParkController : Controller
    {
        // GET: Park
        public ActionResult Index()
        {
            var service = new ParkService();
            return View(service.GetParks());
        }

        private Data.ApplicationDbContext db = new Data.ApplicationDbContext();

        //Create
        public ActionResult Create()
        {
            ViewBag.TeamId = new SelectList(db.Teams, "TeamId", "TeamName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ParkCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = new ParkService();

            if (service.CreatePark(model))
            {
                TempData["SaveResult"] = "New Park added";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Park could not be added");
            return View(model);
        }

        //Details
        public ActionResult Details(int id)
        {
            var service = new ParkService();
            return View(service.GetParkById(id));
        }

        //Edit
        public ActionResult Edit(int id)
        {
            var service = new ParkService();
            var detail = service.GetParkById(id);
            var model =
                new ParkEdit
                {
                    ParkId = detail.ParkId,
                    ParkName = detail.ParkName,
                    CityName = detail.CityName
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ParkEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.ParkId != id)
            {
                ModelState.AddModelError("", "Ids are mistmatched.");
                return View(model);
            }

            var service = new ParkService();

            if (service.UpdatePark(model))
            {
                TempData["SaveResult"] = "Park was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Park could not be updated.");
            return View(model);
        }

        //Delete
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = new ParkService();
            return View(service.GetParkById(id));
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = new ParkService();
            service.DeletePark(id);
            TempData["SaveResult"] = "Park has been deleted.";
            return RedirectToAction("Index");
        }
    }
}