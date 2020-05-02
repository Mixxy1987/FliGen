using FliGen.Services.Seasons.Application.Dto;
using FliGen.Services.Seasons.Application.Queries.Tours;
using RestEase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FliGen.Services.Seasons.Application.Services
{
    [SerializationMethods(Query = QuerySerializationMethod.Serialized)]
    public interface IToursService
    {
        [AllowAnyStatusCode]
        [Get("tours/playerId/{playerId}/seasons")]
        Task<IEnumerable<TourDto>> GetAsync(
            [Path]int playerId, 
            [Query(Name = "id")]int[] seasonsId,
            [Query]ToursQueryType queryType,
            [Query]int last);

        [AllowAnyStatusCode]
        [Get("tours/season/{id}/stats")]
        Task<SeasonStatsDto> GetSeasonStatsAsync([Path]int id);
    }
}