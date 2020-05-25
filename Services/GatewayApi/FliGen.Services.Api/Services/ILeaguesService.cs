using FliGen.Services.Api.Models;
using FliGen.Services.Api.Models.Leagues;
using FliGen.Services.Api.Queries.Leagues;
using RestEase;
using System.Collections.Generic;
using System.Threading.Tasks;
using FliGen.Common.Types;


namespace FliGen.Services.Api.Services
{
    [SerializationMethods(Query = QuerySerializationMethod.Serialized)]
    public interface ILeaguesService
    {
        [AllowAnyStatusCode]
        [Get("ping")]
        Task Ping();

        [AllowAnyStatusCode]
        [Get("leagues")]
        Task<PagedResult<League>> GetAsync([Query]LeaguesQuery leaguesQuery);

        [AllowAnyStatusCode]
        [Get("leagues/info")]
        Task<LeaguesShortInfo> GetLeaguesShortInfoAsync([Query]LeaguesShortInfoQuery query);

        [AllowAnyStatusCode]
        [Get("leagues/information")]
        Task<LeagueInformation> GetLeagueInformationAsync([Query]LeagueInformationQuery query);

        [AllowAnyStatusCode]
        [Get("leagues/types")]
        Task<IEnumerable<League>> GetLeagueTypesAsync([Query]LeagueTypesQuery leaguesQuery);

        [AllowAnyStatusCode]
        [Get("leagues/settings")]
        Task<LeagueSettings> GetLeagueSettingsAsync([Query]LeagueSettingsQuery leaguesQuery);

        [AllowAnyStatusCode]
        [Get("leagues/joinedPlayers")]
        Task<IEnumerable<PlayerInternalId>> GetLeagueJoinedPlayersAsync([Query]LeagueJoinedPlayersQuery query);
    }
}