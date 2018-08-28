using Microsoft.AspNet.Identity;
using StadiumTracker.Data;
using StadiumTracker.Models;
using StadiumTracker.Models.VisitorModels;
using StadiumTracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StadiumTracker.WebMVC.Controllers
{
    [Authorize]
    public class VisitorController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

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
            int jan = 0, feb = 0, mar = 0, apr = 0, may = 0, jun = 0, jul = 0, aug = 0, sep = 0, oct = 0, nov = 0, dec = 0;
            int aLeague = 0, nLeague = 0;

            var visitList = db.Visits.Where(p => p.VisitorId == id).ToList();

            foreach (Visit visit in visitList)
            {
                switch (visit.VisitDate.Month)
                {
                    case 1:
                        jan++;
                        break;
                    case 2:
                        feb++;
                        break;
                    case 3:
                        mar++;
                        break;
                    case 4:
                        apr++;
                        break;
                    case 5:
                        may++;
                        break;
                    case 6:
                        jun++;
                        break;
                    case 7:
                        jul++;
                        break;
                    case 8:
                        aug++;
                        break;
                    case 9:
                        sep++;
                        break;
                    case 10:
                        oct++;
                        break;
                    case 11:
                        nov++;
                        break;
                    case 12:
                        dec++;
                        break;
                }
                foreach (Team team in (db.Teams.Where(e => e.ParkId == visit.ParkId)))
                {
                    if (team.League.LeagueName == "National") nLeague++;
                    else aLeague++;
                }
            }


            ViewBag.MonthData = ($"{jan},{feb},{mar},{apr},{may},{jun},{jul},{aug},{sep},{oct},{nov},{dec},0");
            ViewBag.LeagueData = ($"{nLeague},{aLeague}");

            var service = CreateVisitorService();
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