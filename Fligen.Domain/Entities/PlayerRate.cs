using System;

namespace Fligen.Domain.Entities
{
    public class PlayerRate
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Rate { get; set; }

        public int PlayerId { get; set; }
        public Player Player { get; set; }
    }
}
