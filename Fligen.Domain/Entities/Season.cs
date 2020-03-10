using System;
using System.Collections.Generic;

namespace FliGen.Domain.Entities
{
    public class Season
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }

        public int LeagueId { get; set; }
        public League League { get; set; }
        public List<Tour> Tours { get; set; }
    }
}
