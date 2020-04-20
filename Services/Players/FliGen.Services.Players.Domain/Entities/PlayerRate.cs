using System;
using FliGen.Common.Extensions;
using FliGen.Common.SeedWork;
using FliGen.Common.Types;
using FliGen.Services.Players.Domain.Common;

namespace FliGen.Services.Players.Domain.Entities
{
    public class PlayerRate : Entity
    {
        public DateTime Date { get; }
        public double Value { get; }

        public int PlayerId { get; }
        public Player Player { get; }

        public int LeagueId { get; }

        protected PlayerRate(){}

        public PlayerRate(DateTime date, double rate) : this()
        {
            //todo:: validation?
            Date = date;
            Value = rate;
        }

        public PlayerRate(DateTime date, string rate, int playerId, int leagueId) : this()
        {
            if (date > DateTime.UtcNow)
            {
                throw new FliGenException(ErrorCodes.InvalidDate, "Cannot create player rate with future date");
            }

            if (string.IsNullOrWhiteSpace(rate))
            {
                throw new FliGenException(ErrorCodes.InvalidRateValue, "Cannot create player rate with empty value");
            }

            if (playerId <= 0)
            {
                throw new FliGenException(ErrorCodes.InvalidPlayerId, "Cannot create player with negative id");
            }

            if (leagueId <= 0)
            {
                throw new FliGenException(ErrorCodes.InvalidLeagueId, "Cannot create player with negative league id");
            }


            Date = date;
            Value = double.Parse(rate.CommaToDot());
            PlayerId = playerId;
            LeagueId = leagueId;
        }
    }
}
