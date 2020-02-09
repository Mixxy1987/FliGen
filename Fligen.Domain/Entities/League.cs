using System.Collections.Generic;

namespace FliGen.Domain.Entities
{
    public class League
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Season> Seasons { get; set; }
    }
}
