using FliGen.Common.Types;
using FliGen.Services.Teams.Application.Dto;
using FliGen.Services.Teams.Application.Queries.LeaguesQuery;
using RestEase;
using System.Threading.Tasks;

namespace FliGen.Services.Teams.Application.Services
{
    [SerializationMethods(Query = QuerySerializationMethod.Serialized)]
    public interface ILeaguesService
    {
        [AllowAnyStatusCode]
        [Get("settings/{id}")]
        Task<LeagueSettingsDto> GetLeagueSettingsAsync([Path] int id);

        [AllowAnyStatusCode]
        [Get("leagues")]
        Task<PagedResult<LeagueDto>> GetAsync([Query]LeaguesQuery leaguesQuery);
    }
}