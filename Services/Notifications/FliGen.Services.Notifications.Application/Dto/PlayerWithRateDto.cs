using System.Collections.Generic;

namespace FliGen.Services.Notifications.Application.Dto
{
    public class PlayerWithRateDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<PlayerLeagueRateDto> PlayerLeagueRates { get; set; }
    }
}
