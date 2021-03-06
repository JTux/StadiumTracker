﻿using Microsoft.AspNet.Identity;
using StadiumTracker.Models.VisitorModels;
using StadiumTracker.Services;
using System;
using System.Web.Mvc;

namespace StadiumTracker.WebMVC.Controllers
{
    [Authorize]
    public class VisitorController : Controller
    {
        public ActionResult Index()
        {
            var service = CreateVisitorService();
            return View(service.GetVisitors());
        }

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

        public ActionResult Details(int id)
        {
            var service = CreateVisitorService();

            ViewBag.MonthData = service.GetMonthDataById(id);
            ViewBag.LeagueData = service.GetLeagueDataById(id);

            return View(service.GetVisitorById(id));
        }

        public ActionResult Edit(int id)
        {
            var service = CreateVisitorService();
            var detail = service.GetVisitorById(id);
            var model =
                new VisitorEdit
                {
                    VisitorId = detail.VisitorId,
                    FirstName = detail.FirstName,
                    LastName = detail.LastName,
                    FullName = $"{detail.FirstName} {detail.LastName}"
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