using FliGen.Domain.Common;
using FliGen.Domain.Entities.Enum;
using System.Collections.Generic;

namespace FliGen.Domain.Entities
{
    public class League : Entity, IAggregateRoot
    {
        public string Name { get; }
        public string Description { get; }
        
        public LeagueSettings LeagueSettings { get; }
        public int LeagueTypeId {
            get
            {
                return Type.Id;
            }
            set
            {
                Type = Enumeration.FromValue<LeagueType>(value);
            }
        }
        public LeagueType Type { get; private set; }

        public virtual ICollection<LeaguePlayerLink> LeaguePlayerLinks { get; }
        public List<Season> Seasons { get; }

        protected League()
        {
        }

        private League(string name, string description, LeagueType type) : this()
        {
            Name = name;
            Description = description;
            Type = type;
            Seasons = new List<Season>();
            LeagueSettings = new LeagueSettings(true, false);
        }

        public static League Create(string name, string description, LeagueType type)
        {
            return new League(name, description, type);
        }

        public static League GetUpdated(int id, string firstName, string lastName, LeagueType type)
        {
            return new League(firstName, lastName, type) { Id = id };
        }
    }
}
