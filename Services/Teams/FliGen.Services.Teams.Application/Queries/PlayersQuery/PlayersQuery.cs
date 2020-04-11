namespace FliGen.Services.Teams.Application.Queries.PlayersQuery
{
    public class PlayersQuery 
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public PlayersQueryType QueryType { get; set; }
        public int[] PlayerId { get; set; }
        public int[] LeagueId { get; set; }
    }
}
