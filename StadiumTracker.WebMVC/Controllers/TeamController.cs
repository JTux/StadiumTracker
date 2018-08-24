﻿using StadiumTracker.Data;
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

        public ActionResult Create()
        {
            ViewBag.LeagueNames = new SelectList(Enum.GetValues(typeof(League)), "League");
            
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
            var service = new TeamService();
            var detail = service.GetTeamById(id);
            var model =
                new TeamEdit
                {
                    TeamId = detail.TeamId,
                    TeamName = detail.TeamName,
                    League = detail.League
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TeamEdit model)
        {
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