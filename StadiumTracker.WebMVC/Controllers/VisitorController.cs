using Microsoft.AspNet.Identity;
using StadiumTracker.Models;
using StadiumTracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StadiumTracker.WebMVC.Controllers
{
    public class VisitorController : Controller
    {
        // GET: Visitor
        public ActionResult Index()
        {
            var service = CreateVisitorService();
            return View(service.GetVisitors());
        }

        //Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VisitorCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateVisitorService();

            if (service.CreateVisitor(model))
            {
                TempData["SaveResult"] = "New visitor added.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Visitor could not be added.");
            return View(model);
        }

        //Details
         public ActionResult Details(int id)
        {
            var service = CreateVisitorService();
            return View(service.GetVisitorById(id));
        }

        //Edit
        public ActionResult Edit(int id)
        {
            var service = CreateVisitorService();
            var detail = service.GetVisitorById(id);
            var model =
                new VisitorEdit
                {
                    VisitorId = detail.VisitorId,
                    FirstName = detail.FirstName,
                    LastName = detail.LastName
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, VisitorEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.VisitorId != id)
            {
                ModelState.AddModelError("", "Ids are mismatched.");
                return View(model);
            }

            var service = CreateVisitorService();

            if (service.UpdateVisitor(model))
            {
                TempData["SaveResult"] = "Visitor was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Visitor could not be updated.");
            return View(model);
        }

        //Delete
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = CreateVisitorService();
            return View(service.GetVisitorById(id));
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateVisitorService();
            service.DeleteVisitor(id);
            TempData["SaveResult"] = "Visitor was deleted.";
            return RedirectToAction("Index"); 
        }

        private VisitorService CreateVisitorService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            return new VisitorService(userId);
        }
    }
}