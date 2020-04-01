using FliGen.Services.Tours.Application.Dto;
using RestEase;
using System.Threading.Tasks;

namespace FliGen.Services.Tours.Application.Services
{
    public interface ITeamsService
    {
        [AllowAnyStatusCode]
        [Get("teams/toursByPlayerId")]
        Task<ToursByPlayerIdDto> GetToursByPlayerIdAsync([Query]int size, [Query]int page, [Query]int playerId);
    }
}