using System;
using FliGen.Common.Extensions;
using FliGen.Common.SeedWork;
using FliGen.Common.Types;

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
                throw new FliGenException("cannot_create_player_rate_with_future_date", "Cannot create player rate with future date");
            }

            if (string.IsNullOrWhiteSpace(rate))
            {
                throw new FliGenException("cannot_create_player_rate_with_empty_value", "Cannot create player rate with empty value");
            }

            if (playerId <= 0)
            {
                throw new FliGenException("cannot_create_player_with_negative_id", "Cannot create player with negative id");
            }

            if (leagueId <= 0)
            {
                throw new FliGenException("cannot_create_player_with_negative_league_id", "Cannot create player with negative league id");
            }


            Date = date;
            Value = double.Parse(rate.CommaToDot());
            PlayerId = playerId;
            LeagueId = leagueId;
        }
    }
}
