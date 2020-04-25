namespace FliGen.Services.Leagues.IntegrationTests.Fixtures
{
    public class MockedData
    {
        public int LeagueForDeleteId { get; set; }
        public int LeagueForUpdateId { get; set; }

        public int Player1 { get; set; }
        public int Player2 { get; set; }
        public int Player3 { get; set; }
        public int Player4 { get; set; }
        public int Player5 { get; set; }

        public int LeagueForJoinId1 { get; set; }
        public int LeagueForJoinId2 { get; set; }
        public int LeagueForJoinId3 { get; set; }

        public int League1JoinedPlayersCount { get; set; }
        public int League2JoinedPlayersCount { get; set; }
        public int League3JoinedPlayersCount { get; set; }

        public int League1WaitingPlayersCount { get; set; }
        public int League2WaitingPlayersCount { get; set; }
        public int League3WaitingPlayersCount { get; set; }

        public int PlayersInTeam { get; set; }
        public int TeamsInTour { get; set; }
    }
}