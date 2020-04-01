using FliGen.Services.Api.Models.Players;
using FliGen.Services.Api.Queries;
using RestEase;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace FliGen.Services.Api.Services
{
    [SerializationMethods(Query = QuerySerializationMethod.Serialized)]
    public interface IPlayersService
    {
        [AllowAnyStatusCode]
        [Get("players")]
        Task<IEnumerable<PlayerWithRate>> GetAsync([Query]PlayersQuery playersQuery);
    }
}