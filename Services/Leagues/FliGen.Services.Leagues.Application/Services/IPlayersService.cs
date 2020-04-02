using System.Threading.Tasks;
using FliGen.Services.Leagues.Application.Dto;
using RestEase;

namespace FliGen.Services.Leagues.Application.Services
{
    public interface IPlayersService
    {
        [AllowAnyStatusCode]
        [Get("players/id")]
        Task<PlayerInternalIdDto> GetInternalIdAsync([Query]string externalId);
    }
}