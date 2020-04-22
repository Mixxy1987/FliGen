using FliGen.Common.Messages;
using FliGen.Services.Leagues.Application.Dto.Enum;
using Newtonsoft.Json;

namespace FliGen.Services.Leagues.Application.Events.PlayerLeagueStatus
{
    public class PlayerLeagueJoinStatusChanged : IEvent
    {
        public int PlayerId { get; set; }
        public int LeagueId { get; set; }
        public PlayerLeagueJoinStatus PlayerLeagueJoinNewStatus { get; set; }


        [JsonConstructor]
        public PlayerLeagueJoinStatusChanged(int playerId, int leagueId, PlayerLeagueJoinStatus playerLeagueJoinNewStatus)
        {
            PlayerId = playerId;
            LeagueId = leagueId;
            PlayerLeagueJoinNewStatus = playerLeagueJoinNewStatus;
        }
    }
}