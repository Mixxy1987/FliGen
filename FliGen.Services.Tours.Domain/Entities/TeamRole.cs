using FliGen.Common.SeedWork;

namespace FliGen.Services.Tours.Domain.Entities
{
    public class TeamRole : Enumeration
    {
        public static TeamRole Home = new TeamRole(1, nameof(Home));
        public static TeamRole Guest = new TeamRole(2, nameof(Guest));

        public TeamRole(int id, string name) : base(id, name)
        {
        }
    }
}