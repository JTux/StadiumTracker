using Microsoft.AspNet.Identity;
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
            var service = CreateTeamService();
            return View(service.GetTeams());
        }

        public ActionResult Create()
        {
            var service = CreateTeamService();

            var newLeagueList = new SelectList(service.GetOwnedList("League"), "LeagueId", "LeagueName");
            var sortedLeagueList = newLeagueList.OrderBy(o => o.Text);

            var newTeamList = new SelectList(service.GetOwnedList("Team"), "TeamId", "TeamName");
            var sortedTeamList = newTeamList.OrderBy(o => o.Text);

            ViewBag.TeamId = sortedTeamList;
            ViewBag.LeagueId = sortedLeagueList;
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TeamCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateTeamService();

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
            var service = CreateTeamService();
            return View(service.GetTeamById(id));
        }

        public ActionResult Edit(int id)
        {
            var service = CreateTeamService();
            var detail = service.GetTeamById(id);
            var model =
                new TeamEdit
                {
                    TeamId = detail.TeamId,
                    TeamName = detail.TeamName,
                };

            var newLeagueList = new SelectList(service.GetOwnedList("League"), "LeagueId", "LeagueName").ToList();
            var sortedLeagueList = newLeagueList.OrderBy(o => o.Text);

            var newTeamList = new SelectList(service.GetOwnedList("Team"), "TeamId", "TeamName").ToList();
            var sortedTeamList = newTeamList.OrderBy(o => o.Text);

            ViewBag.LeagueId = sortedLeagueList;
            ViewBag.TeamId = sortedTeamList;
            ViewBag.LeagueInfo = detail.LeagueName;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TeamEdit model)
        {
            var service = CreateTeamService();

            ViewBag.LeagueId = new SelectList(service.GetOwnedList("League"), "LeagueId", "LeagueName");
            ViewBag.TeamId = new SelectList(service.GetOwnedList("Team"), "TeamId", "TeamName");

            if (!ModelState.IsValid) return View(model);

            if(model.TeamId != id)
            {
                ModelState.AddModelError("", "Ids are mismatched.");
                return View(model);
            }

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
            var service = CreateTeamService();
            return View(service.GetTeamById(id));
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateTeamService();
            service.DeleteTeam(id);
            TempData["SaveResult"] = "Team was deleted.";
            return RedirectToAction("Index");
        }

        private TeamService CreateTeamService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            return new TeamService(userId);
        }
    }
}