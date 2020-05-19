using FliGen.Services.Api.Models.Seasons;
using RestEase;
using System.Collections.Generic;
using System.Threading.Tasks;


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
        Task<IEnumerable<Season>> GetAsync([Path(Name = "id")]int leagueId, [Query(Name = "id")]int[] seasonsId);

    }
}