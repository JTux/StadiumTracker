using StadiumTracker.Data;
using StadiumTracker.Models.LeagueModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadiumTracker.Services
{
    public class LeagueService
    {
        public bool CreateLeague(LeagueCreate model)
        {
            var entity =
                new League()
                {
                    LeagueName = model.LeagueName,
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Leagues.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<LeagueListItem> GetLeagues()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Leagues
                        .Select(
                            e =>
                            new LeagueListItem
                            {
                                LeagueId = e.LeagueId,
                                LeagueName = e.LeagueName
                            }
                        );
                return query.ToArray();
            }
        }

        public LeagueDetail GetLeagueById(int leagueId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Leagues
                        .Single(e => e.LeagueId == leagueId);
                return
                    new LeagueDetail
                    {
                        LeagueId = entity.LeagueId,
                        LeagueName = entity.LeagueName
                    };
            }
        }

        public bool UpdateLeague(LeagueEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Leagues
                        .Single(e => e.LeagueId == model.LeagueId);

                entity.LeagueName = model.LeagueName;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteLeague(int leagueId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Leagues
                        .Single(e => e.LeagueId == leagueId);

                ctx.Leagues.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
