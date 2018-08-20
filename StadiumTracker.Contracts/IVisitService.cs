using StadiumTracker.Models.VisitModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadiumTracker.Contracts
{
    public interface IVisitService
    {
        bool CreateVisit(VisitCreate model);
        IEnumerable<VisitListItem> GetVisits();
        VisitDetail GetVisitById(int visitId);
        bool UpdateVisit(VisitEdit model);
        bool DeleteVisit(int visitId);
    }
}
