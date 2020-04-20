namespace FliGen.Services.Tours.Domain.Common
{
    public static class ErrorCodes
    {
        public const string NoPlayerWithSuchId = "there_is_no_player_with_such_external_id";
        public const string NoLeagueWithSuchId = "there_is_no_league_with_id";
        public const string PlayerIsNotAMemberOfLeague = "player_is_not_a_member_of_this_league";
        public const string InvalidTourId = "invalid_tour_id";
        public const string TourRegistrationIsNotYetOpened = "tour_registration_is_not_yet_opened";
        public const string TourRegistrationIsClosed = "tour_registration_is_closed";
        public const string PlayerAlreadyRegistered = "player_already_registered";
        public const string EmptyPlayersList = "players_list_is_empty";
        public const string InvalidSeasonId = "invalid_season_id";
        public const string InvalidDate = "invalid_date";
        public const string InvalidPlayerId = "invalid_player_id";
    }
}