﻿using FliGen.Common.Types;

namespace FliGen.Services.Teams.Domain.Entities
{
    public class TemporalTeamPlayerLink
    {
        public int TourId { get; set; }
        public int LeagueId { get; set; }
        public int TeamId { get; set; }
        public int PlayerId { get; set; }

        private TemporalTeamPlayerLink(int tourId, int leagueId, int teamId, int playerId)
        {
            if (tourId <= 0 ||
                leagueId <= 0 ||
                teamId <= 0 ||
                playerId <= 0)
            {
                throw new FliGenException("invalid_data_for_team_player_link", "Invalid data for team player link");
            }

            TourId = tourId;
            LeagueId = leagueId;
            TeamId = teamId;
            PlayerId = playerId;
        }

        public static TemporalTeamPlayerLink Create(int tourId, int leagueId, int teamId, int playerId)
        {
            return new TemporalTeamPlayerLink(tourId, leagueId, teamId, playerId);
        }
    }
}