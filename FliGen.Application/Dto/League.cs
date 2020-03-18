namespace FliGen.Application.Dto
{
    public class League
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public LeagueType LeagueType { get; set; }
        public PlayerLeagueJoinStatus PlayerLeagueJoinStatus { get; set; } = PlayerLeagueJoinStatus.None;
    }
}