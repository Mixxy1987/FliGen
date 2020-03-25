using FliGen.Common.SeedWork;

namespace FliGen.Domain.Entities
{
    public class LeagueSettings : Entity
    {
        public int LeagueId { get; private set; }
        public League League { get; set; }

        public bool Visibility { get; }
        public bool RequireConfirmation { get; }

        protected LeagueSettings() { }

        public LeagueSettings(bool visibility, bool requireConfirmation) : this()
        {
            Visibility = visibility;
            RequireConfirmation = requireConfirmation;
        }

        public static LeagueSettings GetUpdated(LeagueSettings oldSettings, bool visibility, bool requireConfirmation)
        {
            return new LeagueSettings(visibility, requireConfirmation)
            {
                Id = oldSettings.Id,
                LeagueId = oldSettings.LeagueId
            };
        }
    }
}