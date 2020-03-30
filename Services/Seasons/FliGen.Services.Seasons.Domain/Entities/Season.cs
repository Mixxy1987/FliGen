using System;

namespace FliGen.Services.Seasons.Domain.Entities
{
    public class Season
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }

        public int LeagueId { get; set; }
    }
}
