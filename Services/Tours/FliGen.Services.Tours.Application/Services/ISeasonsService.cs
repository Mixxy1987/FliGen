using FliGen.Services.Tours.Application.Dto;
using FliGen.Services.Tours.Application.Queries.LeaguesSeasonsIdQuery;
using RestEase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FliGen.Services.Tours.Application.Services
{
    [SerializationMethods(Query = QuerySerializationMethod.Serialized)]
    public interface ISeasonsService
    {
        [AllowAnyStatusCode]
        [Get("seasons/leaguesSeasonsId")]
        Task<IEnumerable<LeaguesSeasonsIdDto>> GetLeaguesSeasonsId(
            [Query(Name = "leagueId")] int[] leaguesId,
            [Query(Name = "seasonId")] int[] seasonsId,
            LeaguesSeasonsIdQueryType queryType);
    }
}