using System.Collections;
using System.Collections.Generic;
using StadiumTracker.Models.TeamModels;

namespace StadiumTracker.Contracts
{
    public interface ITeamService
    {
        bool CreateTeam(TeamCreate model);
        bool DeleteTeam(int teamId);
        IEnumerable GetOwnedList(string choice);
        TeamDetail GetTeamById(int teamId);
        IEnumerable<TeamListItem> GetTeams();
        bool UpdateTeam(TeamEdit model);
    }
}