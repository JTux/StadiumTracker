using StadiumTracker.Data;
using StadiumTracker.Models.VisitModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadiumTracker.Services
{
    public class VisitService
    {
        private readonly Guid _userId;
        public VisitService(Guid userId)
        {
            _userId = userId;
        }

        //Create
        public bool CreateVisit(VisitCreate model)
        {
            var entity = new Visit()
            {
                OwnerId = _userId,
                VisitDate = model.VisitDate,
                Park = model.Park,
                Visitor = model.Visitor,
                GotPin = model.GotPin,
                GotPhoto = model.GotPhoto,
                ParkId = model.ParkId,
                VisitorId = model.VisitorId
            };

            using (var ctx = new ApplicationDbContext())
            {
                UpdateTotalVisits(entity.VisitorId, 1, ctx);
                UpdateVisitCount(entity.ParkId, 1, ctx);

                if (entity.GotPin == true) 
                    UpdatePinCount(entity.ParkId, 1, ctx);
                if (entity.GotPhoto == true) 
                    UpdatePhotoCount(entity.ParkId, 1, ctx);

                ctx.Visits.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        //Get All Visits
        public IEnumerable<VisitListItem> GetVisits()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Visits
                        .Select(
                            e =>
                                new VisitListItem
                                {
                                    VisitId = e.VisitId,
                                    ParkId = e.ParkId,
                                    Park = e.Park,
                                    VisitorId = e.VisitorId,
                                    Visitor = e.Visitor,
                                    GotPhoto = e.GotPhoto,
                                    GotPin = e.GotPin,
                                    VisitDate = e.VisitDate
                                }
                        );
                return query.ToArray();
            }
        }

        //Get Visit By ID
        public VisitDetail GetVisitById(int visitId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Visits
                        .Single(e => e.VisitId == visitId);
                return
                    new VisitDetail
                    {
                        VisitId = entity.VisitId,
                        Park = entity.Park,
                        Visitor = entity.Visitor,
                        VisitDate = entity.VisitDate,
                        GotPin = entity.GotPin,
                        GotPhoto = entity.GotPhoto
                    };
            }
        }

        //Update Visit
        public bool UpdateVisit(VisitEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Visits
                        .Single(e => e.VisitId == model.VisitId);

                var parkBoolCheck = AccessPark(ctx, entity.Park);


                if (model.GotPin != parkBoolCheck.HasPin)
                {
                    if (parkBoolCheck.HasPin == true) UpdatePinCount(entity.ParkId, -1, ctx);
                    else if (parkBoolCheck.HasPin == false) UpdatePinCount(entity.ParkId, 1, ctx);
                }

                if (model.GotPhoto != parkBoolCheck.HasPhoto)
                {
                    if (parkBoolCheck.HasPhoto == true) UpdatePhotoCount(entity.ParkId, -1, ctx);
                    else if (parkBoolCheck.HasPhoto == false) UpdatePhotoCount(entity.ParkId, 1, ctx);
                }

                entity.Visitor = model.Visitor;
                entity.VisitDate = model.VisitDate;
                entity.GotPin = model.GotPin;
                entity.GotPhoto = model.GotPhoto;

                return ctx.SaveChanges() == 1;
            }
        }

        //Delete Visit
        public bool DeleteVisit(int visitId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Visits
                        .Single(e => e.VisitId == visitId);

                var visitCountCheck =
                    ctx
                        .Visitors
                        .Single(e => e.VisitorId == entity.VisitorId);
                if (visitCountCheck.TotalVisits > 0)
                    UpdateTotalVisits(entity.VisitorId, -1, ctx);

                var parkBoolCheck =
                    ctx
                        .Parks
                        .Single(e => e.ParkId == entity.ParkId);

                if (parkBoolCheck.VisitCount > 0)
                    UpdateVisitCount(parkBoolCheck.ParkId, -1, ctx);

                if (parkBoolCheck.PinCount > 0)
                    UpdatePinCount(parkBoolCheck.ParkId, -1, ctx);

                if (parkBoolCheck.PhotoCount > 0)
                    UpdatePhotoCount(parkBoolCheck.ParkId, -1, ctx);

                ctx.Visits.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        private bool UpdateTotalVisits(int visitorId, int value, ApplicationDbContext ctx)
        {
            var entity =
                ctx
                    .Visitors
                    .Single(e => e.VisitorId == visitorId);
            entity.TotalVisits += value;
            return ctx.SaveChanges() == 1;
        }

        private Park AccessPark(ApplicationDbContext ctx, Park park)
        {
            var variable =
                ctx
                    .Parks
                    .Single(e => e.ParkId == park.ParkId);
            return variable;
                            
        }

        private bool UpdateVisitCount(int parkId, int value, ApplicationDbContext ctx)
        {
            var entity =
                ctx
                    .Parks
                    .Single(e => e.ParkId == parkId);
            entity.VisitCount += value;

            var park = AccessPark(ctx, entity);

            if (park.VisitCount > 0) park.IsVisited = true;
            else park.IsVisited = false;

            return ctx.SaveChanges() == 1;
        }

        private bool UpdatePinCount(int parkId, int value, ApplicationDbContext ctx)
        {
            var entity =
                ctx
                    .Parks
                    .Single(e => e.ParkId == parkId);
            entity.PinCount += value;

            var park = AccessPark(ctx,entity);

            if (park.PinCount > 0) park.HasPin = true;
            else park.HasPin = false;

            return ctx.SaveChanges() == 1;
        }

        private bool UpdatePhotoCount(int parkId, int value, ApplicationDbContext ctx)
        {
            var entity =
                ctx
                    .Parks
                    .Single(e => e.ParkId == parkId);
            entity.PhotoCount += value;

            var park = AccessPark(ctx, entity);

            if (park.PhotoCount > 0) park.HasPhoto = true;
            else park.HasPhoto = false;

            return ctx.SaveChanges() == 1;
        }
    }
}