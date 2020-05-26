using FliGen.Services.Api.Models;
using FliGen.Services.Api.Models.Tours;
using FliGen.Services.Api.Queries.Tours;
using RestEase;
using System.Collections.Generic;
using System.Threading.Tasks;
using FliGen.Common.Types;


namespace FliGen.Services.Api.Services
{
    [SerializationMethods(Query = QuerySerializationMethod.Serialized)]
    public interface IToursService
    {
        [AllowAnyStatusCode]
        [Get("Ping")]
        Task Ping();

        [AllowAnyStatusCode]
        [Get("tours/id")]
        Task<Tour> GetAsync([Query]TourByIdQuery query);

        [AllowAnyStatusCode]
        [Get("tours/player/{playerId}")]
        Task<PagedResult<Tour>> GetAsync([Path]int playerId, [Query]ToursQuery query);

        [AllowAnyStatusCode]
        [Get("tours/player")]
        Task<PagedResult<Tour>> GetAsync([Query] ToursQuery query);

        [AllowAnyStatusCode]
        [Get("tours/player/{playerId}/seasons")]
        Task<PagedResult<Tour>> GetWithSeasonsAsync(
            [Path]int playerId,
            [Query(Name = "id")]int[] seasonsId,
            [Query]ToursQueryType queryType,
            [Query]int? last,
            [Query]int? size,
            [Query]int? page);

        [AllowAnyStatusCode]
        [Get("tours/registeredOnTourPlayers")]
        Task<IEnumerable<PlayerInternalId>> GetAsync([Query]RegisteredOnTourPlayers query);
    }
}