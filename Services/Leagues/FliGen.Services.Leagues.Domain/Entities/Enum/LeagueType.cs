using FliGen.Common.SeedWork;

namespace FliGen.Services.Leagues.Domain.Entities.Enum
{
    public class LeagueType : Enumeration
    {
        public static LeagueType None = new LeagueType(Enum.None, nameof(None));
        public static LeagueType Football = new LeagueType(Enum.Football, nameof(Football));
        public static LeagueType Hockey = new LeagueType(Enum.Hockey, nameof(Hockey));

        public LeagueType(int id, string name) : base(id, name)
        {
        }

        public class Enum
        {
	        public const int None = 1;
	        public const int Football = 2;
	        public const int Hockey = 3;
        }
    }
}