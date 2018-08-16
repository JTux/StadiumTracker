using Microsoft.AspNet.Identity;
using StadiumTracker.Data;
using StadiumTracker.Models.VisitModels;
using StadiumTracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StadiumTracker.WebMVC.Controllers
{
    [Authorize]
    public class VisitController : Controller
    {
        // GET: Visit
        public ActionResult Index()
        {
            var service = CreateVisitService();
            return View(service.GetVisits());
        }

        private ApplicationDbContext db = new ApplicationDbContext();

        //Create
        public ActionResult Create()
        {
            ViewBag.VisitorId = new SelectList(db.Visitors, "VisitorId", "FirstName");
            ViewBag.ParkId = new SelectList(db.Parks, "ParkId", "ParkName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VisitCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateVisitService();

            if (service.CreateVisit(model))
            {
                TempData["SaveResult"] = "New Visit Added";
                return RedirectToAction("Index");
            }

            //ViewBag.VisitorId = new SelectList(db.Visitors, "VisitorId", "FirstName");
            ViewBag.ParkId = new SelectList(db.Parks, "ParkId", "ParkName", model.ParkId);

            ModelState.AddModelError("", "Visit could not be added.");
            return View(model);
        }

        //Details
        public ActionResult Details(int id)
        {
            var service = CreateVisitService();
            return View(service.GetVisitById(id));
        }

        //Edit
        public ActionResult Edit (int id)
        {
            var service = CreateVisitService();
            var detail = service.GetVisitById(id);
            var model =
                new VisitEdit
                {
                    VisitId = detail.VisitId,
                    Park = detail.Park,
                    Visitor = detail.Visitor,
                    VisitDate = detail.VisitDate,
                    GotPin = detail.GotPin,
                    GotPhoto = detail.GotPhoto
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, VisitEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.VisitId != id)
            {
                ModelState.AddModelError("", "Ids are mismatched.");
                return View(model);
            }

            var service = CreateVisitService();

            if (service.UpdateVisit(model))
            {
                TempData["SaveResult"] = "Visit was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Visit could not be updated.");
            return View(model);
        }

        //Delete
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = CreateVisitService();
            return View(service.GetVisitById(id));
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateVisitService();
            service.DeleteVisit(id);
            TempData["SaveResult"] = "Visit has been deleted.";
            return RedirectToAction("Index");
        }

        //Refactor Creating Service into a single Method
        private VisitService CreateVisitService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            return new VisitService(userId);
        }
    }
}