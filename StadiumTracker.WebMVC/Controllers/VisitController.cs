﻿using Microsoft.AspNet.Identity;
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
        public ActionResult Index()
        {
            var service = CreateVisitService();
            return View(service.GetVisits());
        }

        public ActionResult Create()
        {
            var service = CreateVisitService();
            var visitorService = new VisitorService(Guid.Parse(User.Identity.GetUserId()));

            var unsortedVisitorList = new SelectList(visitorService.GetVisitors(), "VisitorId", "FullName");
            var visitorList = unsortedVisitorList.OrderBy(o => o.Text);

            var unsortedParkList = new SelectList(service.GetOwnedList("Park"), "ParkId", "ParkName");
            var parkList = unsortedParkList.OrderBy(o => o.Text);

            var unsortedTeamList = new SelectList(service.GetOwnedList("Team"), "TeamId", "TeamName");
            var teamList = unsortedTeamList.OrderBy(o => o.Text);

            ViewBag.VisitorId = visitorList;
            ViewBag.ParkId = parkList;
            ViewBag.HomeTeamId = teamList;
            ViewBag.AwayTeamId = teamList;

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

            ModelState.AddModelError("", "Visit could not be added.");
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var service = CreateVisitService();
            return View(service.GetVisitById(id));
        }

        public ActionResult Edit(int id)
        {
            var service = CreateVisitService();

            var unsortedVisitorList = new SelectList(service.GetOwnedList("Visitor"), "VisitorId", "FullName");
            var visitorList = unsortedVisitorList.OrderBy(o => o.Text);

            var unsortedParkList = new SelectList(service.GetOwnedList("Park"), "ParkId", "ParkName");
            var parkList = unsortedParkList.OrderBy(o => o.Text);

            var unsortedTeamList = new SelectList(service.GetOwnedList("Team"), "TeamId", "TeamName");
            var teamList = unsortedTeamList.OrderBy(o => o.Text);

            ViewBag.VisitorId = visitorList;
            ViewBag.ParkId = parkList;
            ViewBag.HomeTeamId = teamList;
            ViewBag.AwayTeamId = teamList;

            var detail = service.GetVisitById(id);
            var model =
                new VisitEdit
                {
                    VisitId = detail.VisitId,
                    ParkName = detail.ParkName,
                    HomeTeamName = detail.HomeTeamName,
                    AwayTeamName = detail.AwayTeamName,
                    VisitorFirstName = detail.VisitorFirstName,
                    VisitorLastName = detail.VisitorLastName,
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

        private VisitService CreateVisitService()
        {
            var ownerId = Guid.Parse(User.Identity.GetUserId());
            return new VisitService(ownerId);
        }
    }
}