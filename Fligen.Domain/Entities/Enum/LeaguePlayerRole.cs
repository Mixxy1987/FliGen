namespace FliGen.Domain.Entities.Enum
{
    public class LeaguePlayerRole : Common.Enumeration
    {
        public static LeaguePlayerRole User = new LeaguePlayerRole(1, nameof(User));
        public static LeaguePlayerRole Admin = new LeaguePlayerRole(2, nameof(Admin));

        public LeaguePlayerRole(int id, string name) : base(id, name)
        {
        }
    }
}