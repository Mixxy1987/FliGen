using System.Collections.Generic;

namespace FliGen.Services.Teams.Application.Services.GenerateTeams
{
    public class InfoForGenerate
    {
        public int TeamsCount { get; set; }
        public int PlayersInTeamCount { get; set; }
        public IEnumerable<PlayerInfoForGenerate> PlayersInfo { get; set; }

        public InfoForGenerate(int teamsCount, int playersInTeamCount, IEnumerable<PlayerInfoForGenerate> playersInfo)
        {
            TeamsCount = teamsCount;
            PlayersInTeamCount = playersInTeamCount;
            PlayersInfo = playersInfo;
        }
    }
}