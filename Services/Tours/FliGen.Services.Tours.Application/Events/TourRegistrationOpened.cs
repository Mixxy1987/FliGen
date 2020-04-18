using System;
using FliGen.Common.Messages;
using Newtonsoft.Json;

namespace FliGen.Services.Tours.Application.Events
{
    public class TourRegistrationOpened : IEvent
    {
        public int LeagueId { get; set; }
        public int TourId { get; set; }
        public DateTime Date { get; set; }

        [JsonConstructor]
        public TourRegistrationOpened(int tourId, int leagueId, DateTime date)
        {
            LeagueId = leagueId;
            TourId = tourId;
            Date = date;
        }
    }
}