using System.Threading.Tasks;
using FliGen.Services.Tours.Application.Dto;
using RestEase;

namespace FliGen.Services.Tours.Application.Services
{
    public interface IPlayersService
    {
        [AllowAnyStatusCode]
        [Get("players/id")]
        Task<PlayerInternalIdDto> GetInternalIdAsync([Query]string externalId);
    }
}