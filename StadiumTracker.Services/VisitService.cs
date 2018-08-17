﻿using StadiumTracker.Data;
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

            UpdateTotalVisits(entity.VisitorId, 1);

            using (var ctx = new ApplicationDbContext())
            {
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

                entity.Park = model.Park;
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

                var countCheck =
                    ctx
                        .Visitors
                        .Single(e => e.VisitorId == entity.VisitorId);

                if (countCheck.TotalVisits > 0)
                    UpdateTotalVisits(entity.VisitorId, -1);

                ctx.Visits.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        private bool UpdateTotalVisits(int visitorId, int value)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Visitors
                        .Single(e => e.VisitorId == visitorId);
                entity.TotalVisits += value;
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
