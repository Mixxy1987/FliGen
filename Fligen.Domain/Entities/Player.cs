using FliGen.Common.Extensions;
using FliGen.Domain.Common;
using System;
using System.Collections.Generic;

namespace FliGen.Domain.Entities
{
    public class Player: Entity, IAggregateRoot
    {
        public string FirstName { get; }
        public string LastName { get; }
        public List<PlayerRate> Rates { get; }

        public virtual ICollection<LeaguePlayerLink> LeaguePlayerLinks { get; }
        protected Player(){}

        private Player(string firstName, string lastName, double rate) : this()
        {
            FirstName = firstName;
            LastName = lastName;
            Rates = new List<PlayerRate>
            {
                new PlayerRate(DateTime.Now, rate)
            };
        }

        public static Player Create(string firstName, string lastName, string rate)
        {
            return new Player(firstName, lastName, double.Parse(rate.DotToComma()));
        }

        public static Player GetUpdated(int id, string firstName, string lastName, string rate)
        {
            return new Player(firstName, lastName, double.Parse(rate.DotToComma())) { Id = id };
        }
    }
}