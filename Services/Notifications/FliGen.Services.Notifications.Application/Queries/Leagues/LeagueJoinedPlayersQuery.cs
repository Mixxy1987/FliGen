namespace FliGen.Services.Notifications.Application.Queries.Leagues
{
    public class LeagueJoinedPlayersQuery
    {
        public int LeagueId { get; }

        public LeagueJoinedPlayersQuery(int leagueId)
        {
            LeagueId = leagueId;
        }
    }
}