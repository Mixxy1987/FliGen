using FliGen.Services.Api.Models.Players;
using FliGen.Services.Api.Queries.Players;
using RestEase;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace FliGen.Services.Api.Services
{
    [SerializationMethods(Query = QuerySerializationMethod.Serialized)]
    public interface IPlayersService
    {
        [AllowAnyStatusCode]
        [Get("Ping")]
        Task Ping();

        [AllowAnyStatusCode]
        [Get("players")]
        Task<IEnumerable<PlayerWithRate>> GetAsync([Query]PlayersQuery query);

        [AllowAnyStatusCode]
        [Get("players/info")]
        Task<PlayersInfo> GetPlayersInfoAsync([Query]PlayersInfoQuery query);
    }
}