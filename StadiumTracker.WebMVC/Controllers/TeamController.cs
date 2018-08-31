using StadiumTracker.Data;
using StadiumTracker.Models.TeamModels;
using StadiumTracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StadiumTracker.WebMVC.Controllers
{
    public class TeamController : Controller
    {
        public ActionResult Index()
        {
            var service = new TeamService();
            return View(service.GetTeams());
        }

        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Create()
        {
            var newParkList = new SelectList(db.Parks, "ParkId", "ParkName").ToList();
            var sortedParkList = newParkList.OrderBy(o => o.Text);

            var newLeagueList = new SelectList(db.Leagues, "LeagueId", "LeagueName").ToList();
            var sortedLeagueList = newLeagueList.OrderBy(o => o.Text);

            ViewBag.LeagueId = sortedLeagueList;
            ViewBag.ParkId = sortedParkList;
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TeamCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = new TeamService();

            if (service.CreateTeam(model))
            {
                TempData["SaveResult"] = "New team added.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Team could not be created.");
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var service = new TeamService();
            return View(service.GetTeamById(id));
        }

        public ActionResult Edit(int id)
        {
            ViewBag.LeagueId = new SelectList(db.Leagues, "LeagueId", "LeagueName");
            ViewBag.ParkId = new SelectList(db.Parks, "ParkId", "ParkName");

            var service = new TeamService();
            var detail = service.GetTeamById(id);
            var model =
                new TeamEdit
                {
                    TeamId = detail.TeamId,
                    TeamName = detail.TeamName,
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TeamEdit model)
        {
            ViewBag.LeagueId = new SelectList(db.Leagues, "LeagueId", "LeagueName");
            ViewBag.ParkId = new SelectList(db.Parks, "ParkId", "ParkName");

            if (!ModelState.IsValid) return View(model);

            if(model.TeamId != id)
            {
                ModelState.AddModelError("", "Ids are mismatched.");
                return View(model);
            }

            var service = new TeamService();

            if (service.UpdateTeam(model))
            {
                TempData["SaveResult"] = "Team was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Team could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = new TeamService();
            return View(service.GetTeamById(id));
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = new TeamService();
            service.DeleteTeam(id);
            TempData["SaveResult"] = "Team was deleted.";
            return RedirectToAction("Index");
        }
    }
}