using System;
using System.Collections.Generic;
using FliGen.Domain.Common;

namespace FliGen.Domain.Entities
{
    public class Player: Entity, IAggregateRoot
    {
        public string FirstName { get; }
        public string LastName { get; }
        public List<PlayerRate> Rates { get; }

        protected Player(){}

        private Player(string firstName, string lastName, double rate) : this()
        {
            FirstName = firstName;
            LastName = lastName;
            Rates = new List<PlayerRate>()
            {
                new PlayerRate(DateTime.Now, rate)
            };
        }

        public static Player Create(string firstName, string lastName, double rate)
        {
            return new Player(firstName, lastName, rate);
        }

        public static Player GetUpdated(int id, string firstName, string lastName, double rate)
        {
            return new Player(firstName, lastName, rate) { Id = id };
        }
    }
}