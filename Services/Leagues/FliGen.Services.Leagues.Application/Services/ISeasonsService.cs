using FliGen.Services.Leagues.Application.Dto;
using FliGen.Services.Leagues.Application.Dto.Enum;
using RestEase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FliGen.Services.Leagues.Application.Services
{
    public interface ISeasonsService
    {
        [AllowAnyStatusCode]
        [Get("players/id")]
        Task<PlayerInternalIdDto> GetInternalIdAsync([Query]string externalId);

        [AllowAnyStatusCode]
        [Get("seasons/league/{id}/seasons")]
        public Task<IEnumerable<SeasonDto>> GetAsync(
            [Path(Name = "id")] int leagueId,
            [Query(Name = "id")] int[] seasonsId,
            [Query]SeasonsQueryType queryType);
    }
}