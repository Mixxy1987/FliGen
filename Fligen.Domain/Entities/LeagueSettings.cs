using FliGen.Domain.Common;

namespace FliGen.Domain.Entities
{
    public class LeagueSettings : Entity
    {
        public int LeagueId { get; set; }
        public League League { get; set; }

        public bool Visibility { get; }
        public bool RequireConfirmation { get; }

        protected LeagueSettings() { }

        public LeagueSettings(bool visibility, bool requireConfirmation) : this()
        {
            Visibility = visibility;
            RequireConfirmation = requireConfirmation;
        }
    }
}