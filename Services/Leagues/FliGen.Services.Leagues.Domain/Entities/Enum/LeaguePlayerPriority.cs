using FliGen.Common.SeedWork;

namespace FliGen.Services.Leagues.Domain.Entities.Enum
{
    public class LeaguePlayerPriority : Enumeration
    {
        public static LeaguePlayerPriority Highest = new LeaguePlayerPriority(Enum.Highest, nameof(Highest));
        public static LeaguePlayerPriority High = new LeaguePlayerPriority(Enum.High, nameof(High));
        public static LeaguePlayerPriority Normal = new LeaguePlayerPriority(Enum.Normal, nameof(Normal));
        public static LeaguePlayerPriority Low = new LeaguePlayerPriority(Enum.Low, nameof(Low));
        public static LeaguePlayerPriority Lowest = new LeaguePlayerPriority(Enum.Lowest, nameof(Lowest));

        public LeaguePlayerPriority(int id, string name) : base(id, name)
        {
        }

        public class Enum
        {
	        public const int Highest = 1;
            public const int High = 2;
            public const int Normal = 3;
            public const int Low = 4; 
            public const int Lowest = 5;
        }
    }
}