using FliGen.Common.SeedWork;

namespace FliGen.Services.Teams.Domain.Entities
{
    public class TeamRole : Enumeration
    {
        public static readonly TeamRole Home = new TeamRole(Enum.Home, nameof(Home));
        public static readonly TeamRole Guest = new TeamRole(Enum.Guest, nameof(Guest));

        public TeamRole(int id, string name) : base(id, name)
        {
        }

        public class Enum
        {
            public const int Home = 1;
            public const int Guest = 2;
        }
    }
}