using FliGen.Services.Api.Models;
using FliGen.Services.Api.Models.Tours;
using FliGen.Services.Api.Queries.Tours;
using RestEase;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace FliGen.Services.Api.Services
{
    [SerializationMethods(Query = QuerySerializationMethod.Serialized)]
    public interface IToursService
    {
        [AllowAnyStatusCode]
        [Get("tours/id")]
        Task<Tour> GetAsync([Query]TourByIdQuery query);

        [AllowAnyStatusCode]
        [Get("tours/playerId/{playerId}")]
        Task<IEnumerable<Tour>> GetAsync([Path]int playerId, [Query]ToursByPlayerIdQuery query);

        [AllowAnyStatusCode]
        [Get("tours/playerId/{playerId}/seasons")]
        Task<IEnumerable<Tour>> GetWithSeasonsAsync([Path]int playerId, [Query]ToursByPlayerIdQuery query);

        [AllowAnyStatusCode]
        [Get("tours/registeredOnTourPlayers")]
        Task<IEnumerable<PlayerInternalId>> GetAsync([Query]RegisteredOnTourPlayers query);
    }
}