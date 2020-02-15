using System.Collections.Generic;
using FliGen.Domain.Common;

namespace FliGen.Domain.Entities
{
    public class League : Entity, IAggregateRoot
    {
        public string Name { get; }
        public string Description { get; }
        public LeagueType LeagueType { get; }
        public List<Season> Seasons { get; }

        protected League()
        {
        }

        private League(string name, string description, LeagueType leagueType) : this()
        {
            Name = name;
            Description = description;
            LeagueType = leagueType;
            Seasons = new List<Season>();
        }

        public static League Create(string name, string description, LeagueType leagueType)
        {
            return new League(name, description, leagueType);
        }
    }
}
