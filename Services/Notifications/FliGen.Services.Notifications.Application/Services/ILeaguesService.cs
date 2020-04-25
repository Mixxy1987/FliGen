using FliGen.Services.Notifications.Application.Dto;
using FliGen.Services.Notifications.Application.Queries;
using RestEase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FliGen.Services.Notifications.Application.Services
{
    [SerializationMethods(Query = QuerySerializationMethod.Serialized)]
    public interface ILeaguesService
    {
        [AllowAnyStatusCode]
        [Get("leagues/joinedPlayers")]
        Task<IEnumerable<PlayerInternalIdDto>> GetLeagueJoinedPlayersAsync([Query]LeagueJoinedPlayersQuery query);
    }
}