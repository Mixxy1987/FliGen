using FliGen.Common.SeedWork.Repository;
using FliGen.Services.Seasons.Application.Dto;
using FliGen.Services.Seasons.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FliGen.Services.Seasons.Application.Queries.LeaguesSeasonsIdQuery
{
    public class LeaguesSeasonsIdQueryHandler : IRequestHandler<LeaguesSeasonsIdQuery, IEnumerable<LeaguesSeasonsIdDto>>
    {
        private readonly IUnitOfWork _uow;

        public LeaguesSeasonsIdQueryHandler(IUnitOfWork uow)
        {
	        _uow = uow;
        }

        public async Task<IEnumerable<LeaguesSeasonsIdDto>> Handle(LeaguesSeasonsIdQuery request, CancellationToken cancellationToken)
        {
            if (request.SeasonsId == null && request.LeaguesId == null)
            {
                return null;
            }

            if (request.SeasonsId != null && 
                request.SeasonsId.Length != 0)
            {
                return FilterBySeasonsId(request.SeasonsId);
            }

            return FilterByLeaguesId(request);
        }

        private IEnumerable<LeaguesSeasonsIdDto> FilterBySeasonsId(int[] seasonsId)
        {
            var seasonsRepo = _uow.GetReadOnlyRepository<Season>();

            var seasons = seasonsRepo.GetList(
                s => seasonsId.Contains(s.Id),
                size: seasonsId.Length).Items;

            IEnumerable<LeaguesSeasonsIdDto> dtos = seasons.Select(s => new LeaguesSeasonsIdDto()
            {
                SeasonId = s.Id,
                LeagueId = s.LeagueId
            });

            return dtos;
        }

        private IEnumerable<LeaguesSeasonsIdDto> FilterByLeaguesId(LeaguesSeasonsIdQuery request)
        {
            var seasonsRepo = _uow.GetReadOnlyRepository<Season>();

            List<Season> seasons;
            if (request.LeaguesSeasonsIdQueryType == LeaguesSeasonsIdQueryType.All)
            {
                seasons = seasonsRepo.GetList( //todo:: paged queries
                    s => request.LeaguesId.Contains(s.LeagueId)).Items.ToList();
            }
            else
            {
                seasons = seasonsRepo.GetList(
                    s => request.LeaguesId.Contains(s.LeagueId) &&
                         s.Finish >= DateTime.UtcNow).Items.ToList();
            }

            IEnumerable<LeaguesSeasonsIdDto> dtos = seasons.Select(s => new LeaguesSeasonsIdDto()
            {
                SeasonId = s.Id,
                LeagueId = s.LeagueId
            });

            return dtos;
        }
    }
}
