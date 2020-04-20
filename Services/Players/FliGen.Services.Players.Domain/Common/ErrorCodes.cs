namespace FliGen.Services.Players.Domain.Common
{
    public static class ErrorCodes
    {
        public const string EmptyFirstName = "cannot_create_player_with_empty_firstName";
        public const string EmptyLastName = "cannot_create_player_with_empty_lastName";
        public const string InvalidDate = "invalid_date";
        public const string InvalidRateValue = "invalid_rate_value"; 
        public const string InvalidPlayerId = "invalid_player_id";
        public const string InvalidLeagueId = "invalid_league_id";
    }
}