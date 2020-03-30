using FliGen.Common.SeedWork;

namespace FliGen.Services.Leagues.Domain.Entities.Enum
{
    public class LeaguePlayerRole : Enumeration
    {
        public static LeaguePlayerRole User = new LeaguePlayerRole(Enum.User, nameof(User));
        public static LeaguePlayerRole Admin = new LeaguePlayerRole(Enum.Admin, nameof(Admin));

        public LeaguePlayerRole(int id, string name) : base(id, name)
        {
        }

        public class Enum
        {
	        public const int User = 1;
	        public const int Admin = 2;
        }
    }
}