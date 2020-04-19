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
        [Get("tours/{id}")]
        Task<Tour> Get([Path] int id);

        [AllowAnyStatusCode]
        [Get("tours")]
        Task<IEnumerable<Tour>> Get([Query]ToursByPlayerIdQuery query);

        [AllowAnyStatusCode]
        [Get("tours/registeredOnTourPlayers")]
        Task<IEnumerable<PlayerInternalId>> Get([Query]RegisteredOnTourPlayers query);
    }
}