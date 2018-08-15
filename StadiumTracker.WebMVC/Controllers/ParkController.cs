using Microsoft.AspNet.Identity;
using StadiumTracker.Models.Park;
using StadiumTracker.Services;
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
            var service = CreateParkService();
            return View(service.GetParks());
        }

        //Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ParkCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateParkService();

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
            var service = CreateParkService();
            return View(service.GetParkById(id));
        }

        //Edit
        public ActionResult Edit(int id)
        {
            var service = CreateParkService();
            var detail = service.GetParkById(id);
            var model =
                new ParkEdit
                {
                    ParkId = detail.ParkId,
                    ParkName = detail.ParkName,
                    TeamName = detail.TeamName
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

            var service = CreateParkService();

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
            var service = CreateParkService();
            return View(service.GetParkById(id));
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateParkService();
            service.DeletePark(id);
            TempData["SaveResult"] = "Park has been deleted.";
            return RedirectToAction("Index");
        }

        private ParkService CreateParkService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            return new ParkService(userId);
        }
    }
}