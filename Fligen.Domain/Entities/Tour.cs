using System;

namespace FliGen.Domain.Entities
{
    public class Tour
    {
        public int Id { get; set; }
        public DateTime TourDate { get; set; }
        public int HomeTeamId { get; set; }
        public int GuestTeamId { get; set; }

        public int HomeCount { get; set; }
        public int GuestCount { get; set; }

        public int SeasonId { get; set; }
        public Season Season { get; set; }
    }
}
