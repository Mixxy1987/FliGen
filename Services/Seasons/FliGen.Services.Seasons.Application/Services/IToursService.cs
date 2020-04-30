using FliGen.Services.Seasons.Application.Dto;
using RestEase;
using System.Threading.Tasks;

namespace FliGen.Services.Seasons.Application.Services
{
    public interface IToursService
    {
        [AllowAnyStatusCode]
        [Get("")]
        Task<TourDto> GetAsync();
    }
}