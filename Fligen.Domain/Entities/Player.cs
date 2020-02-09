using System.Collections.Generic;

namespace FliGen.Domain.Entities
{
    public class Player
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<PlayerRate> Rates { get; set; }
    }
}