using StadiumTracker.Models.ParkModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StadiumTracker.Contracts
{
    public interface IParkService
    {
        bool CreatePark(ParkCreate model);
        IEnumerable<ParkListItem> GetParks();
        ParkDetail GetParkById(int parkId);
        bool UpdatePark(ParkEdit model);
        bool DeletePark(int parkId);
    }
}
