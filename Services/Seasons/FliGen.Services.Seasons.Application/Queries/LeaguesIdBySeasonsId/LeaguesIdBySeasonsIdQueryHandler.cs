using FliGen.Common.SeedWork.Repository;
using FliGen.Services.Seasons.Application.Dto;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FliGen.Services.Seasons.Domain.Entities;

namespace FliGen.Services.Seasons.Application.Queries.LeaguesIdBySeasonsId
{
    public class LeaguesIdBySeasonsIdQueryHandler : IRequestHandler<LeaguesIdBySeasonsIdQuery, IEnumerable<LeagueIdBySeasonIdDto>>
    {
        private readonly IUnitOfWork _uow;

        public LeaguesIdBySeasonsIdQueryHandler(IUnitOfWork uow)
        {
	        _uow = uow;
        }

        public async Task<IEnumerable<LeagueIdBySeasonIdDto>> Handle(LeaguesIdBySeasonsIdQuery request, CancellationToken cancellationToken)
        {
            var seasonsRepo = _uow.GetReadOnlyRepository<Season>();

            var seasons = seasonsRepo.GetList(
                s => request.SeasonsId.Contains(s.Id),
                size: request.SeasonsId.Length).Items;

            IEnumerable<LeagueIdBySeasonIdDto> dtos = seasons.Select(s => new LeagueIdBySeasonIdDto()
            {
                SeasonId = s.Id,
                LeagueId = s.LeagueId
            });

            return dtos;
        }
    }
}
