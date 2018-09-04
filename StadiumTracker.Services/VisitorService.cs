using StadiumTracker.Contracts;
using StadiumTracker.Data;
using StadiumTracker.Models.VisitorModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadiumTracker.Services
{
    public class VisitorService : IVisitorService
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private readonly Guid _userId;
        public VisitorService() { }
        public VisitorService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateVisitor(VisitorCreate model)
        {
            var entity =
                new Visitor()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    FullName = $"{model.FirstName} {model.LastName}"
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Visitors.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<VisitorListItem> GetVisitors()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Visitors
                        .Select(
                            e =>
                                new VisitorListItem
                                {
                                    VisitorId = e.VisitorId,
                                    FirstName = e.FirstName,
                                    LastName = e.LastName,
                                    FullName = e.FullName,
                                    TotalVisits = e.TotalVisits,
                                    TotalPins = e.TotalPins
                                }
                        );
                return query.ToArray();
            }
        }

        public VisitorDetail GetVisitorById(int visitorId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Visitors
                        .Single(e => e.VisitorId == visitorId);
                return
                    new VisitorDetail
                    {
                        VisitorId = entity.VisitorId,
                        FirstName = entity.FirstName,
                        LastName = entity.LastName,
                        FullName = entity.FullName,
                        TotalVisits = entity.TotalVisits,
                        TotalPins = entity.TotalPins
                    };
            }
        }

        public bool UpdateVisitor(VisitorEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Visitors
                        .Single(e => e.VisitorId == model.VisitorId);

                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.FullName = $"{model.FirstName} {model.LastName}";

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteVisitor(int visitorId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Visitors
                        .Single(e => e.VisitorId == visitorId);

                ctx.Visitors.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public string GetLeagueDataById(int id)
        {
            int aLeague = 0, nLeague = 0;
            var visitList = GetVisitsById(id);

            foreach (Visit visit in visitList)
            {
                if (visit.VisitorId == id)
                {
                    foreach (Team team in (db.Teams.Where(e => e.TeamId == visit.HomeTeamId)))
                    {
                        if (team.LeagueId == 1) nLeague++;
                        else aLeague++;
                    }
                }
            }

            return ($"{nLeague},{aLeague}");
        }

        public string GetMonthDataById(int id)
        {
            int jan = 0, feb = 0, mar = 0, apr = 0, may = 0, jun = 0, jul = 0, aug = 0, sep = 0, oct = 0, nov = 0, dec = 0;
            var visitList = GetVisitsById(id);

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
            }

            return ($"{jan},{feb},{mar},{apr},{may},{jun},{jul},{aug},{sep},{oct},{nov},{dec},0");
        }

        private List<Visit> GetVisitsById(int id)
        {
            var visitList = db.Visits.Where(p => p.VisitorId == id).ToList();
            return visitList;
        }
    }
}