using FliGen.Domain.Common;

namespace FliGen.Domain.Entities
{
    public class LeagueType : Enumeration
    {
        public static LeagueType None = new LeagueType(0, "None");
        public static LeagueType Football = new LeagueType(1, "Football");
        public static LeagueType Hockey = new LeagueType(2, "Hockey");
        
        public LeagueType(int id, string name) : base(id, name)
        {
        }
    }
}