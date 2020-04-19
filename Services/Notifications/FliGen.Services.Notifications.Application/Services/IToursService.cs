using FliGen.Services.Notifications.Application.Dto;
using FliGen.Services.Notifications.Application.Queries.Tours;
using RestEase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FliGen.Services.Notifications.Application.Services
{
    [SerializationMethods(Query = QuerySerializationMethod.Serialized)]
    public interface IToursService
    {
        [AllowAnyStatusCode]
        [Get("tours/registeredOnTourPlayers")]
        Task<IEnumerable<PlayerInternalIdDto>> GetAsync([Query]RegisteredOnTourPlayers query);
    }
}