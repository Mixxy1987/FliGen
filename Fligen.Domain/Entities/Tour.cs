using System;
using System.Collections.Generic;
using FliGen.Domain.Common;

namespace FliGen.Domain.Entities
{
    public class Tour : Entity
    {
        public DateTime Date { get; set; }
        public List<Team> Teams { get; set; }
        public int? HomeCount { get; set; }
        public int? GuestCount { get; set; }

        public int SeasonId { get; set; }
        public Season Season { get; set; }
    }
}
