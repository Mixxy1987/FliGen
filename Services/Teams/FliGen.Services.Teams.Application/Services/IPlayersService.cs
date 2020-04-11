using FliGen.Services.Teams.Application.Dto;
using FliGen.Services.Teams.Application.Queries.PlayersQuery;
using RestEase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FliGen.Services.Teams.Application.Services
{
    [SerializationMethods(Query = QuerySerializationMethod.Serialized)]
    public interface IPlayersService
    {
        [AllowAnyStatusCode]
        [Get("players")]
        Task<IEnumerable<PlayerWithRateDto>> GetAsync([Query]PlayersQuery playersQuery);
    }
}