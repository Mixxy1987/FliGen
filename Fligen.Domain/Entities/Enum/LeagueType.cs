namespace FliGen.Domain.Entities.Enum
{
    public class LeagueType : Common.Enumeration
    {
        public static LeagueType None = new LeagueType(1, nameof(None));
        public static LeagueType Football = new LeagueType(2, nameof(Football));
        public static LeagueType Hockey = new LeagueType(3, nameof(Hockey));

        public LeagueType(int id, string name) : base(id, name)
        {
        }
    }
}