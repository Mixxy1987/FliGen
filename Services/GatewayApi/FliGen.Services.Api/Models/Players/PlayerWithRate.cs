using System.Collections.Generic;

namespace FliGen.Services.Api.Models.Players
{
    public class PlayerWithRate
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<PlayerLeagueRate> PlayerLeagueRates { get; set; }
    }
}