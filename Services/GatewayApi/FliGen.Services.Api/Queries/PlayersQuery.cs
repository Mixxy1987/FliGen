using Microsoft.AspNetCore.Mvc;

namespace FliGen.Services.Api.Queries
{
    public class PlayersQuery : PagedQuery
    {
        public int QueryType { get; set; }
        public int[] PlayerId { get; set; }
        public int[] LeagueId { get; set; }
    }
}