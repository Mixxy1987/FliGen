using System;
using FliGen.Common.Extensions;
using FliGen.Common.SeedWork;

namespace FliGen.Services.Players.Domain.Entities
{
    public class PlayerRate : Entity
    {
        public DateTime Date { get; }
        public double Value { get; }

        public int PlayerId { get; set; }
        public Player Player { get; set; }

        public int LeagueId { get; set; }

        protected PlayerRate(){}

        public PlayerRate(DateTime date, double rate) : this()
        {
            //todo:: validation?
            Date = date;
            Value = rate;
        }

        public PlayerRate(DateTime date, string rate, int playerId) : this()
        {
            //todo:: validation?
            Date = date;
            Value = double.Parse(rate.CommaToDot());
            PlayerId = playerId;
        }
    }
}
