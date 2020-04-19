using FliGen.Services.Api.Models;
using FliGen.Services.Api.Models.Leagues;
using FliGen.Services.Api.Queries.Leagues;
using RestEase;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace FliGen.Services.Api.Services
{
    [SerializationMethods(Query = QuerySerializationMethod.Serialized)]
    public interface ILeaguesService
    {
        [AllowAnyStatusCode]
        [Get("HealthCheck")]
        Task HealthCheck();

        [AllowAnyStatusCode]
        [Get("leagues")]
        Task<IEnumerable<League>> GetAsync([Query]LeaguesQuery leaguesQuery);

        [AllowAnyStatusCode]
        [Get("leagues/settings")]
        Task<LeagueSettings> GetLeagueSettingsAsync([Query]LeagueSettingsQuery leaguesQuery);

        [AllowAnyStatusCode]
        [Get("leagues/leagueJoinedPlayers")]
        Task<IEnumerable<PlayerInternalId>> GetLeagueJoinedPlayersAsync([Query]LeagueJoinedPlayersQuery query);
    }
}