using FliGen.Common.Extensions;
using FliGen.Common.SeedWork;
using FliGen.Common.Types;
using FliGen.Services.Players.Domain.Common;
using System;
using System.Collections.Generic;

namespace FliGen.Services.Players.Domain.Entities
{
    public class Player: Entity, IAggregateRoot
    {
	    private const string DefaultRate = "7.0";

        public string FirstName { get; }
        public string LastName { get; }
        public string ExternalId { get; }
        public List<PlayerRate> Rates { get; }

        public Player()
        {}

        private Player(string firstName, string lastName, double rate, string externalId) : this()
        {
	        if (string.IsNullOrWhiteSpace(firstName))
	        {
		        throw new FliGenException(ErrorCodes.EmptyFirstName, "Cannot create player with empty first Name");
	        }

	        if (string.IsNullOrWhiteSpace(lastName))
	        {
		        throw new FliGenException(ErrorCodes.EmptyLastName, "Cannot create player with empty last Name");
	        }

            FirstName = firstName;
            LastName = lastName;
            ExternalId = externalId;

            Rates = new List<PlayerRate>
            {
                new PlayerRate(DateTime.UtcNow, rate)
            };
        }

        private Player(string firstName, string lastName, List<PlayerRate> rates, string externalId) : this()
        {
	        FirstName = firstName;
	        LastName = lastName;
	        ExternalId = externalId;
	        Rates = rates;
        }

        public static Player Create(string firstName, string lastName, string rate = DefaultRate, string externalId = null)
        {
            rate ??= DefaultRate;
            return new Player(firstName, lastName, double.Parse(rate.CommaToDot()), externalId);
        }

        public static Player GetUpdated(int id, string firstName, string lastName, string externalId = null)
        {
            return new Player(firstName, lastName, null, externalId)
            {
                Id = id
            };
        }

        public static Player GetUpdated(Player player, int id)
        {
	        return new Player(player.FirstName, player.LastName, player.Rates, player.ExternalId)
	        {
		        Id = id
	        };
        }
    }
}