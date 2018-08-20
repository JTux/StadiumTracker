using StadiumTracker.Models.VisitorModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadiumTracker.Contracts
{
    public interface IVisitorService
    {
        bool CreateVisitor(VisitorCreate model);
        IEnumerable<VisitorListItem> GetVisitors();
        VisitorDetail GetVisitorById(int visitorId);
        bool UpdateVisitor(VisitorEdit model);
        bool DeleteVisitor(int visitorId);
    }
}