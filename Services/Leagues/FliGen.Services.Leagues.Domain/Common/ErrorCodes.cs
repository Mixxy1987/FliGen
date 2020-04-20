namespace FliGen.Services.Leagues.Domain.Common
{
    public static class ErrorCodes
    {
        public const string NoLeagueWithSuchId = "there_is_no_league_with_id";
        public const string NoPlayerWithSuchExternalId = "there_is_no_player_with_such_external_id";
        public const string NoLeagueSettings = "there_is_no_league_settings";
        public const string CannotCreateLeagueWithEmptyName = "cannot_create_league_with_empty_name";
        public const string InvalidLeagueId = "invalid_leagueId";
        public const string InvalidPlayerId = "invalid_playerId";
        public const string InvalidPlayersCount = "invalid_players_count";
        public const string InvalidTeamsCount = "invalid_teams_count";
    }
}