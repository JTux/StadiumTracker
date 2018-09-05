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
        public VisitService() { }

        public bool CreateVisit(VisitCreate model)
        {
            var entity = new Visit()
            {
                VisitDate = model.VisitDate,
                ParkId = model.ParkId,
                Park = model.Park,
                HomeTeamId = model.HomeTeamId,
                HomeTeam = model.HomeTeam,
                AwayTeamId = model.AwayTeamId,
                AwayTeam = model.AwayTeam,
                VisitorId = model.VisitorId,
                Visitor = model.Visitor,
                GotPin = model.GotPin,
                GotPhoto = model.GotPhoto,
            };

            using (var ctx = new ApplicationDbContext())
            {
                UpdateTotalVisits(entity.VisitorId, 1, ctx);
                UpdateVisitCount(entity.ParkId, 1, ctx);

                if (entity.GotPin == true)
                {
                    UpdatePinCount(entity.ParkId, 1, ctx);
                    UpdatePersonalPins(entity.VisitorId, 1, ctx);
                }
                if (entity.GotPhoto == true)
                    UpdatePhotoCount(entity.ParkId, 1, ctx);

                ctx.Visits.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

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
                                    HomeTeamId = e.HomeTeamId,
                                    AwayTeamId = e.AwayTeamId,
                                    HomeTeam = ctx.Teams.FirstOrDefault(t => t.TeamId == e.HomeTeamId),
                                    AwayTeam = ctx.Teams.FirstOrDefault(t => t.TeamId == e.AwayTeamId),
                                    GotPhoto = e.GotPhoto,
                                    GotPin = e.GotPin,
                                    VisitDate = e.VisitDate
                                }
                        );
                var newList = query.ToList();

                newList.OrderBy(x => x.Park.ParkName);

                return newList;
            }
        }

        public VisitDetail GetVisitById(int visitId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Visits.Single(e => e.VisitId == visitId);
                return
                    new VisitDetail
                    {
                        VisitId = entity.VisitId,
                        Park = entity.Park,
                        Visitor = entity.Visitor,
                        HomeTeam = ctx.Teams.Single(e=>e.TeamId == entity.HomeTeamId),
                        AwayTeam = ctx.Teams.Single(e => e.TeamId == entity.AwayTeamId),
                        VisitDate = entity.VisitDate,
                        GotPin = entity.GotPin,
                        GotPhoto = entity.GotPhoto
                    };
            }
        }

        public bool UpdateVisit(VisitEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Visits.Single(e => e.VisitId == model.VisitId);

                var parkBoolCheck = ctx.Parks.Single(e => e.ParkId == entity.Park.ParkId);

                entity.Visitor = model.Visitor;
                entity.HomeTeam = model.HomeTeam;
                entity.AwayTeam = model.AwayTeam;

                if (model.GotPin != parkBoolCheck.HasPin)
                {
                    if (parkBoolCheck.HasPin == true)
                    {
                        UpdatePinCount(entity.ParkId, -1, ctx);
                        UpdatePersonalPins(entity.VisitorId, -1, ctx);
                    }
                    else if (parkBoolCheck.HasPin == false)
                    {
                        UpdatePinCount(entity.ParkId, 1, ctx);
                        UpdatePersonalPins(entity.VisitorId, 1, ctx);
                    }
                }

                if (model.GotPhoto != parkBoolCheck.HasPhoto)
                {
                    if (parkBoolCheck.HasPhoto == true) UpdatePhotoCount(entity.ParkId, -1, ctx);
                    else if (parkBoolCheck.HasPhoto == false) UpdatePhotoCount(entity.ParkId, 1, ctx);
                }

                entity.VisitDate = model.VisitDate;
                entity.GotPin = model.GotPin;
                entity.GotPhoto = model.GotPhoto;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteVisit(int visitId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Visits.Single(e => e.VisitId == visitId);

                var visitCountCheck = ctx.Visitors.Single(e => e.VisitorId == entity.VisitorId);
                if (visitCountCheck.TotalVisits > 0)
                    UpdateTotalVisits(entity.VisitorId, -1, ctx);

                var parkBoolCheck = ctx.Parks.Single(e => e.ParkId == entity.Park.ParkId);

                if (parkBoolCheck.VisitCount > 0)
                    UpdateVisitCount(parkBoolCheck.ParkId, -1, ctx);

                if (parkBoolCheck.PinCount > 0)
                    UpdatePinCount(parkBoolCheck.ParkId, -1, ctx);

                if (parkBoolCheck.PhotoCount > 0)
                    UpdatePhotoCount(parkBoolCheck.ParkId, -1, ctx);

                if (entity.GotPin == true)
                    UpdatePersonalPins(entity.VisitorId, -1, ctx);

                ctx.Visits.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        private bool UpdateTotalVisits(int visitorId, int value, ApplicationDbContext ctx)
        {
            var entity = ctx.Visitors.Single(e => e.VisitorId == visitorId);
            entity.TotalVisits += value;
            return ctx.SaveChanges() == 1;
        }

        private bool UpdateVisitCount(int parkId, int value, ApplicationDbContext ctx)
        {
            var park = ctx.Parks.Single(e => e.ParkId == parkId);
            park.VisitCount += value;

            if (park.VisitCount > 0) park.IsVisited = true;
            else park.IsVisited = false;

            return ctx.SaveChanges() == 1;
        }

        private bool UpdatePinCount(int parkId, int value, ApplicationDbContext ctx)
        {
            var park = ctx.Parks.Single(e => e.ParkId == parkId);
            park.PinCount += value;

            if (park.PinCount > 0) park.HasPin = true;
            else park.HasPin = false;

            return ctx.SaveChanges() == 1;
        }

        private bool UpdatePersonalPins(int visitorId, int value, ApplicationDbContext ctx)
        {
            var visitor = ctx.Visitors.Single(e => e.VisitorId == visitorId);
            visitor.TotalPins += value;

            return ctx.SaveChanges() == 1;
        }

        private bool UpdatePhotoCount(int parkId, int value, ApplicationDbContext ctx)
        {
            var park = ctx.Parks.Single(e => e.ParkId == parkId);
            park.PhotoCount += value;

            if (park.PhotoCount > 0) park.HasPhoto = true;
            else park.HasPhoto = false;

            return ctx.SaveChanges() == 1;
        }
    }
}