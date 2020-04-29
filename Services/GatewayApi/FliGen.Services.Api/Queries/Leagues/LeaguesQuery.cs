namespace FliGen.Services.Api.Queries.Leagues
{
    public class LeaguesQuery
    {
        public string PlayerExternalId { get; set; }
        public int[] LeaguesId { get; set; }
        public int[] PlayersId { get; set; }
    }
}