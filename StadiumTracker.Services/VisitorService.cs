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
                    OwnerId = _userId,
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
                        .Where(e => e.OwnerId == _userId)
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
                        .Single(e => e.VisitorId == visitorId && e.OwnerId == _userId);
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
                        .Single(e => e.VisitorId == model.VisitorId && e.OwnerId == _userId);

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
                        .Single(e => e.VisitorId == visitorId && e.OwnerId == _userId);

                ctx.Visitors.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}