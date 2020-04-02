using AutoMapper;
using FliGen.Common.SeedWork.Repository;
using FliGen.Common.SeedWork.Repository.Paging;
using FliGen.Services.Leagues.Application.CommonLogic;
using FliGen.Services.Leagues.Application.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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
            var leagueRepo = _uow.GetRepositoryAsync<Domain.Entities.League>();

            IPaginate<Domain.Entities.League> leagues = 
                await leagueRepo.GetListAsync(
                    include: q => q.Include(league => league.LeaguePlayerLinks),
                    cancellationToken : cancellationToken);


            List<Dto.League> resultLeagues = leagues.Items
	            .Select(x => _mapper.Map<Dto.League>(x))
	            .ToList();

            if (request.PlayerId is null)
            {
                return resultLeagues;
            }

            var playerInternalIdDto = await _playersService.GetInternalIdAsync(request.PlayerId);
            if (playerInternalIdDto is null)
            {
                return null;
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
