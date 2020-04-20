using FliGen.Common.SeedWork;
using FliGen.Common.Types;
using FliGen.Services.Leagues.Domain.Common;

namespace FliGen.Services.Leagues.Domain.Entities
{
    public class LeagueSettings : Entity
    {
        private const int PlayersInTeamMinCount = 2;
        private const int TeamsInTourMinCount = 2;

        public int LeagueId { get; private set; }
        public League League { get; set; }

        public bool Visibility { get; }
        public bool RequireConfirmation { get; }

        public int? PlayersInTeam { get; }
        public int? TeamsInTour { get; }

        protected LeagueSettings() { }

        private LeagueSettings(
            bool visibility,
            bool requireConfirmation,
            int? playersInTeam = null,
            int? teamsInTour = null) : this()
        {
            Visibility = visibility;
            RequireConfirmation = requireConfirmation;
            if (!(playersInTeam is null) && playersInTeam < PlayersInTeamMinCount)
            {
                throw new FliGenException(ErrorCodes.InvalidPlayersCount, $"players count must not be less than {PlayersInTeamMinCount}");
            }

            if (!(teamsInTour is null) && teamsInTour < PlayersInTeamMinCount)
            {
                throw new FliGenException(ErrorCodes.InvalidTeamsCount, $"teams count must not be less than {TeamsInTourMinCount}");
            }
            PlayersInTeam = playersInTeam;
            TeamsInTour = teamsInTour;
        }

        public static LeagueSettings Create(
            bool visibility,
            bool requireConfirmation,
            int? playersInTeam = null,
            int? teamsInTour = null)
        {
            return new LeagueSettings(visibility,requireConfirmation,playersInTeam,teamsInTour);
        }

        public static LeagueSettings GetUpdated(
            LeagueSettings oldSettings, 
            bool visibility,
            bool requireConfirmation,
            int? playersInTeam,
            int? teamsInTour)
        {
            return new LeagueSettings(visibility, requireConfirmation, playersInTeam, teamsInTour)
            {
                Id = oldSettings.Id,
                LeagueId = oldSettings.LeagueId
            };
        }
    }
}