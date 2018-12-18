using StadiumTracker.Contracts;
using StadiumTracker.Data;
using StadiumTracker.Models.ParkModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace StadiumTracker.Services
{
    public class ParkService : IParkService
    {
        private readonly Guid _ownerId;

        public ParkService(Guid ownerId)
        {
            _ownerId = ownerId;
        }

        public bool CreatePark(ParkCreate model)
        {
            var entity = new Park()
            {
                OwnerId = _ownerId,
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
                        .Select(
                            e =>
                                new ParkListItem
                                {
                                    ParkId = e.ParkId,
                                    ParkName = e.ParkName,
                                    CityName = e.CityName,
                                    IsVisited = e.IsVisited,
                                    HasPin = e.HasPin,
                                    HasPhoto = e.HasPhoto,
                                    VisitCount = e.VisitCount,
                                }
                        );
                var queryArray = query.ToArray();
                Array.Sort(queryArray, delegate (ParkListItem park1, ParkListItem park2) { return park1.ParkName.CompareTo(park2.ParkName); });
                return queryArray;
            }
        }

        public ParkDetail GetParkById(int parkId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Parks.Single(e => e.ParkId == parkId);

                return
                    new ParkDetail
                    {
                        ParkId = entity.ParkId,
                        ParkName = entity.ParkName,
                        CityName = entity.CityName,
                        IsVisited = entity.IsVisited,
                    };
            }
        }

        public bool UpdatePark(ParkEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Parks.Single(e => e.ParkId == model.ParkId && e.OwnerId == _ownerId);

                entity.ParkName = model.ParkName;
                entity.CityName = model.CityName;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeletePark(int parkId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Parks.Single(e => e.ParkId == parkId && e.OwnerId == _ownerId);

                ctx.Parks.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}