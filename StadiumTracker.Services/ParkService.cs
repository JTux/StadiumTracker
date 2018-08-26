using StadiumTracker.Data;
using StadiumTracker.Models.ParkModels;
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

        public bool CreatePark(ParkCreate model)
        {
            var entity = new Park()
            {
                OwnerId = _userId,
                ParkName = model.ParkName,
                CityName = model.CityName
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Parks.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

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
                                    CityName = e.CityName,
                                    IsVisited = e.IsVisited,
                                    HasPin = e.HasPin,
                                    HasPhoto = e.HasPhoto
                                }
                        );
                return query.ToArray();
            }
        }
        
        public ParkDetail GetParkById(int parkId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Parks.Single(e => e.ParkId == parkId && e.OwnerId == _userId);
                return
                    new ParkDetail
                    {
                        ParkId = entity.ParkId,
                        ParkName = entity.ParkName,
                        CityName = entity.CityName,
                        IsVisited = entity.IsVisited
                    };
            }
        }

        public bool UpdatePark(ParkEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Parks.Single(e => e.ParkId == model.ParkId && e.OwnerId == _userId);

                entity.ParkName = model.ParkName;
                entity.CityName = model.CityName;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeletePark(int parkId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Parks.Single(e => e.ParkId == parkId && e.OwnerId == _userId);

                ctx.Parks.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}