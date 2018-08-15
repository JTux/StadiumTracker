using StadiumTracker.Data;
using StadiumTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadiumTracker.Services
{
    public class VisitorService
    {
        //Binds user to Visitor objets
        private readonly Guid _userId;
        public VisitorService(Guid userId)
        {
            _userId = userId;
        }

        //Create Visitor
        public bool CreateVisitor(VisitorCreate model)
        {
            var entity =
                new Visitor()
                {
                    OwnerId = _userId,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Visitors.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        //Get all Visitors
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
                                    TotalVisits = e.TotalVisits
                                }
                        );
                return query.ToArray();
            }
        }

        //GetVisitorByID
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
                        TotalVisits = entity.TotalVisits
                    };
            }
        }

        //UpdateVisitor
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

                return ctx.SaveChanges() == 1;
            }
        }

        //DeleteVisitor
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