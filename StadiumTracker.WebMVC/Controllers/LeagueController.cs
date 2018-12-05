using StadiumTracker.Models.LeagueModels;
using StadiumTracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StadiumTracker.WebMVC.Controllers
{
    [Authorize]
    public class LeagueController : Controller
    {
        public ActionResult Index()
        {
            var service = CreateLeagueService();
            return View(service.GetLeagues());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LeagueCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateLeagueService();

            if (service.CreateLeague(model))
            {
                TempData["SaveResult"] = "New visitor added.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "League could not be added.");
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var service = CreateLeagueService();
            return View(service.GetLeagueById(id));
        }

        public ActionResult Edit(int id)
        {
            var service = CreateLeagueService();
            var detail = service.GetLeagueById(id);
            var model =
                new LeagueEdit
                {
                    LeagueId = detail.LeagueId,
                    LeagueName = detail.LeagueName
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, LeagueEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.LeagueId != id)
            {
                ModelState.AddModelError("", "Ids are mismatched");
                return View(model);
            }

            var service = CreateLeagueService();

            if (service.UpdateLeague(model))
            {
                TempData["SaveResult"] = "League has been updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "League could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = CreateLeagueService();
            return View(service.GetLeagueById(id));
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateLeagueService();
            service.DeleteLeague(id);
            TempData["SaveResult"] = "League was deleted.";
            return RedirectToAction("Index");
        }

        private LeagueService CreateLeagueService()
        {
            return new LeagueService();
        }
    }
}