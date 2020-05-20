using FliGen.Services.Api.Models.Seasons;
using RestEase;
using System.Collections.Generic;
using System.Threading.Tasks;
using FliGen.Services.Api.Queries.Seasons;

namespace FliGen.Services.Api.Services
{
    [SerializationMethods(Query = QuerySerializationMethod.Serialized)]
    public interface ISeasonsService
    {
        [AllowAnyStatusCode]
        [Get("Ping")]
        Task Ping();

        [AllowAnyStatusCode]
        [Get("seasons/league/{id}/seasons")]
        Task<IEnumerable<Season>> GetAsync(
            [Path(Name = "id")]int leagueId, 
            [Query(Name = "id")]int[] seasonsId);

        [AllowAnyStatusCode]
        [Get("seasons/leaguesSeasonsId")]
        Task<IEnumerable<LeaguesSeasonsId>> GetAsync(
            [Query(Name = "leagueId")]int[] leaguesId,
            [Query(Name = "seasonId")]int[] seasonsId,
            LeaguesSeasonsIdQueryType queryType);

    }
}