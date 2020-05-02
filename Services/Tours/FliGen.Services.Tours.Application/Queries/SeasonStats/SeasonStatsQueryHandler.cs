using FliGen.Common.SeedWork.Repository;
using FliGen.Services.Tours.Application.Dto;
using FliGen.Services.Tours.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FliGen.Services.Tours.Application.Queries.SeasonStats
{
    public class SeasonStatsQueryHandler : IRequestHandler<SeasonStatsQuery, SeasonStatsDto>
    {
        private readonly IUnitOfWork _uow;

        public SeasonStatsQueryHandler(
            IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Task<SeasonStatsDto> Handle(SeasonStatsQuery request, CancellationToken cancellationToken)
        {
            var toursRepo = _uow.GetReadOnlyRepository<Tour>();

            return Task.FromResult(new SeasonStatsDto
            {
                Count = toursRepo.Count(t => t.SeasonId == request.SeasonId),
                SeasonId = request.SeasonId
            });
        }
    }
}