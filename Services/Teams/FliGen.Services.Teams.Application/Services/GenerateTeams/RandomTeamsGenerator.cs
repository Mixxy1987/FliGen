using System.Linq;

namespace FliGen.Services.Teams.Application.Services.GenerateTeams
{
    public class RandomTeamsGenerator: IGenerateTeamsService
    {
        public int[][] Generate(InfoForGenerate info)
        {
            int[][] result = new int[info.TeamsCount][];
            int count = 0;
            int teamCount = 0;

            for (int i = 0; i < info.TeamsCount; i++)
            {
                var players = info.PlayersInfo.Skip(count).Take(info.PlayersInTeamCount);
                count += info.PlayersInTeamCount;
                
                result[teamCount] = players.Select(pl => pl.Id).ToArray();
                teamCount++;
            }

            return result;
        }
    }
}