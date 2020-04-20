using FliGen.Common.Messages;
using Newtonsoft.Json;
using System;

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
            TourId = tourId;
            LeagueId = leagueId;
            Date = date;
        }

        public TourRegistrationOpened(int tourId)
        {
            TourId = tourId;
        }
    }
}