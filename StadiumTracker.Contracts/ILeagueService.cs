using System.Collections.Generic;
using StadiumTracker.Models.LeagueModels;

namespace StadiumTracker.Contracts
{
    public interface ILeagueService
    {
        bool CreateLeague(LeagueCreate model);
        bool DeleteLeague(int leagueId);
        LeagueDetail GetLeagueById(int leagueId);
        IEnumerable<LeagueListItem> GetLeagues();
        bool UpdateLeague(LeagueEdit model);
    }
}