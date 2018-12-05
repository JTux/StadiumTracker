using StadiumTracker.Data;
using StadiumTracker.Models.TeamModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadiumTracker.Services
{
    public class TeamService
    {
        private readonly Guid _ownerId;

        public TeamService(Guid ownerId)
        {
            _ownerId = ownerId;
        }

        public bool CreateTeam(TeamCreate model)
        {
            var entity = new Team()
            {
                OwnerId = _ownerId,
                TeamName = model.TeamName,
                LeagueId = model.LeagueId,
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Teams.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<TeamListItem> GetTeams()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                       .Teams
                       .Select(
                            e =>
                                new TeamListItem
                                {
                                    TeamId = e.TeamId,
                                    TeamName = e.TeamName,
                                    LeagueId = e.LeagueId,
                                    League = e.League
                                }
                        );
                var queryArray = query.ToArray();
                Array.Sort(queryArray, delegate (TeamListItem team1, TeamListItem team2) { return team1.TeamName.CompareTo(team2.TeamName); });
                return queryArray;
            }
        }

        public TeamDetail GetTeamById(int teamId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Teams.Single(e => e.TeamId == teamId);
                return
                    new TeamDetail
                    {
                        OwnerId = entity.OwnerId,
                        TeamId = entity.TeamId,
                        TeamName = entity.TeamName,
                        League = entity.League
                    };
            }
        }

        public bool UpdateTeam(TeamEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Teams.Single(e => e.TeamId == model.TeamId && e.OwnerId == _ownerId);

                entity.TeamName = model.TeamName;
                entity.League = ctx.Leagues.Single(e => e.LeagueId == model.LeagueId);

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteTeam(int teamId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Teams.Single(e => e.TeamId == teamId && e.OwnerId == _ownerId);

                ctx.Teams.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable GetOwnedList(string choice)
        {
            var blankGuid = Guid.Parse("00000000-0000-0000-0000-000000000000");

            using (var ctx = new ApplicationDbContext())
            {
                if (choice == "Team")
                    return ctx.Teams.Where(t => t.OwnerId == _ownerId || t.OwnerId == blankGuid);
                else if (choice == "League")
                    return ctx.Leagues;
                else throw new Exception();
            }
        }
    }
}