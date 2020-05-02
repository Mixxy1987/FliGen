using FliGen.Services.Tours.Application.Dto;
using MediatR;

namespace FliGen.Services.Tours.Application.Queries.SeasonStats
{

    public class SeasonStatsQuery : IRequest<SeasonStatsDto>
    {
        public int SeasonId { get; set; }
    }
}