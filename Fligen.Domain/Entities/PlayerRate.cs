using System;
using FliGen.Domain.Common;

namespace FliGen.Domain.Entities
{
    public class PlayerRate : Entity
    {
        public DateTime Date { get; }
        public double Value { get; }

        public int PlayerId { get; set; }
        public Player Player { get; set; }

        protected PlayerRate(){}

        public PlayerRate(DateTime date, double rate) : this()
        {
            //todo:: validation?
            Date = date;
            Value = rate;
        }
    }
}
