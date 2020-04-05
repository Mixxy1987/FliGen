using AutoMapper;
using FliGen.Common.SeedWork.Repository;
using FliGen.Common.SeedWork.Repository.Paging;
using FliGen.Common.Types;
using FliGen.Services.Leagues.Application.CommonLogic;
using FliGen.Services.Leagues.Application.Services;
using FliGen.Services.Leagues.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace FliGen.Services.Leagues.Application.Queries.Leagues
{
    public class LeaguesQueryHandler : IRequestHandler<LeaguesQuery, IEnumerable<Dto.League>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IPlayersService _playersService;

        public LeaguesQueryHandler(
            IUnitOfWork uow,
            IMapper mapper,
            IPlayersService playersService)
        {
	        _uow = uow;
	        _mapper = mapper;
            _playersService = playersService;
        }

        public async Task<IEnumerable<Dto.League>> Handle(LeaguesQuery request, CancellationToken cancellationToken)
        {
            var leagueRepo = _uow.GetRepositoryAsync<League>();

            Expression<Func<League, bool>> predicate = null;
            if (!(request.LeagueId is null) && request.LeagueId.Length != 0)
            {
                predicate = league => request.LeagueId.Contains(league.Id);
            }

            IPaginate<League> leagues = 
                await leagueRepo.GetListAsync(
                    predicate: predicate,
                    include: q => q.Include(league => league.LeaguePlayerLinks),
                    cancellationToken : cancellationToken);

            List<Dto.League> resultLeagues = leagues.Items
	            .Select(x => _mapper.Map<Dto.League>(x))
	            .ToList();

            if (request.PlayerExternalId is null)
            {
                return resultLeagues;
            }

            var playerInternalIdDto = await _playersService.GetInternalIdAsync(request.PlayerExternalId);
            if (playerInternalIdDto is null)
            {
                throw new FliGenException("there_is_no_player_with_such_external_id", $"There is no player with external id: {request.PlayerExternalId}");
            }

            foreach (var league in leagues.Items)
            {
                var distinctLinks = league.LeaguePlayerLinks
                    .Where(l => l.PlayerId == playerInternalIdDto.InternalId)
                    .OrderBy(p => p.CreationTime)
                    .GroupBy(p => p.LeagueId)
                    .Select(g => g.Last());

                resultLeagues.EnrichByPlayerLeagueJoinStatus(distinctLinks);
            }

            return resultLeagues;
        }
    }
}
