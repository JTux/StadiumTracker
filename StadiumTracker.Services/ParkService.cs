using StadiumTracker.Data;
using StadiumTracker.Models.Park;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadiumTracker.Services
{
    public class ParkService
    {
        private readonly Guid _userId;
        public ParkService(Guid userId)
        {
            _userId = userId;
        }

        //Create
        public bool CreatePark(ParkCreate model)
        {
            var entity = new Park()
            {
                OwnerId = _userId,
                ParkName = model.ParkName,
                TeamName = model.TeamName,
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Parks.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        //Get all Parks
        public IEnumerable<ParkListItem> GetParks()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Parks
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new ParkListItem
                                {
                                    ParkId = e.ParkId,
                                    ParkName = e.ParkName,
                                    TeamName = e.TeamName,
                                    IsVisited = e.IsVisited
                                }
                        );
                return query.ToArray();
            }
        }
        
        //Get Park by Id
        public ParkDetail GetParkById(int parkId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Parks
                        .Single(e => e.ParkId == parkId && e.OwnerId == _userId);
                return
                    new ParkDetail
                    {
                        ParkId = entity.ParkId,
                        ParkName = entity.ParkName,
                        TeamName = entity.TeamName,
                        IsVisited = entity.IsVisited
                    };
            }
        }

        //UpdatePark
        public bool UpdatePark(ParkEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Parks
                        .Single(e => e.ParkId == model.ParkId && e.OwnerId == _userId);

                entity.ParkName = model.ParkName;
                entity.TeamName = model.TeamName;

                return ctx.SaveChanges() == 1;
            }
        }

        //DeletePark
        public bool DeletePark(int parkId)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Parks
                        .Single(e => e.ParkId == parkId && e.OwnerId == _userId);

                ctx.Parks.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}