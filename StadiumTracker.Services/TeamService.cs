﻿using StadiumTracker.Data;
using StadiumTracker.Models.TeamModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadiumTracker.Services
{
    public class TeamService
    {
        public bool CreateTeam(TeamCreate model)
        {
            var entity = new Team()
            {
                TeamName = model.TeamName,
                LeagueId = model.LeagueId,
                ParkId = model.ParkId,
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Teams.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<TeamListItem> GetTeams()
        {
            using(var ctx = new ApplicationDbContext())
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
                                    ParkId = e.ParkId,
                                    Park = e.Park,
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
            using(var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Teams.Single(e => e.TeamId == teamId);
                return
                    new TeamDetail
                    {
                        TeamId = entity.TeamId,
                        TeamName = entity.TeamName,
                        Park = entity.Park,
                        League = entity.League
                    };
            }
        }

        public bool UpdateTeam(TeamEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Teams.Single(e => e.TeamId == model.TeamId);

                entity.TeamName = model.TeamName;
                entity.Park = ctx.Parks.Single(e=> e.ParkId == model.ParkId);
                entity.League = ctx.Leagues.Single(e => e.LeagueId == model.LeagueId);

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteTeam(int teamId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Teams.Single(e => e.TeamId == teamId);

                ctx.Teams.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}