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
            List<int> userDataList = new List<int>();
            int jan = 0, feb = 0, mar = 0, apr = 0, may = 0, jun = 0, jul = 0, aug = 0, sep = 0, oct = 0, nov = 0, dec = 0;
            foreach (Visit visit in (db.Visits.Where(e => e.VisitorId == id)))
            {
                if (visit.VisitDate.Month == 1) jan++;
                else if (visit.VisitDate.Month == 2) feb++;
                else if (visit.VisitDate.Month == 3) mar++;
                else if (visit.VisitDate.Month == 4) apr++;
                else if (visit.VisitDate.Month == 5) may++;
                else if (visit.VisitDate.Month == 6) jun++;
                else if (visit.VisitDate.Month == 7) jul++;
                else if (visit.VisitDate.Month == 8) aug++;
                else if (visit.VisitDate.Month == 9) sep++;
                else if (visit.VisitDate.Month == 10) oct++;
                else if (visit.VisitDate.Month == 11) nov++;
                else if (visit.VisitDate.Month == 12) dec++;
            }
            ViewBag.UserData = ($"{jan},{feb},{mar},{apr},{may},{jun},{jul},{aug},{sep},{oct},{nov},{dec},0");
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

        public JsonResult StadiumVisitsByPerson(int visitorId)
        {
            List<int> newDataList = new List<int>();
            foreach (Visit visit in db.Visits.Where(e => e.VisitorId == visitorId))
            {
                newDataList.Add(visit.VisitDate.Month);
            }


            Chart chart = new Chart();
            chart.labels = new string[] { "January", "February", "March" };
            chart.datasets = new List<Datasets>();
            List<Datasets> _dataSet = new List<Datasets>();
            _dataSet.Add(new Datasets()
            {
                label = "Current Year",
                data = new int[] { 28, 48, 40, 0 },
                backgroundColor = new string[] { "#FF0000", "#800000", "#808000" },
                borderColor = new string[] { "#0000FF", "#000080", "#999999" },
                borderWidth = "1"
            });
            chart.datasets = _dataSet;
            return Json(chart, JsonRequestBehavior.AllowGet);
        }

        private VisitorService CreateVisitorService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            return new VisitorService(userId);
        }
    }
}