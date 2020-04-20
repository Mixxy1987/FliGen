using AutoMapper;
using FliGen.Common.SeedWork.Repository;
using FliGen.Common.SeedWork.Repository.Paging;
using FliGen.Common.Types;
using FliGen.Services.Leagues.Application.Dto;
using FliGen.Services.Leagues.Application.Dto.Enum;
using FliGen.Services.Leagues.Application.Services;
using FliGen.Services.Leagues.Domain.Common;
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
    public class LeaguesQueryHandler : IRequestHandler<LeaguesQuery, IEnumerable<LeagueDto>>
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

        public async Task<IEnumerable<LeagueDto>> Handle(LeaguesQuery request, CancellationToken cancellationToken)
        {
            var leagueRepo = _uow.GetRepositoryAsync<League>();

            Expression<Func<League, bool>> lPredicate = null;
            if (!(request.LeagueId is null) && request.LeagueId.Length != 0)
            {
                lPredicate = league => request.LeagueId.Contains(league.Id);
            }

            IPaginate<League> leagues = 
                await leagueRepo.GetListAsync(
                    predicate: lPredicate,
                    include: q => q.Include(league => league.LeaguePlayerLinks),
                    cancellationToken : cancellationToken);

            List<LeagueDto> resultLeagues = leagues.Items
	            .Select(x => _mapper.Map<LeagueDto>(x))
	            .ToList();

            Func<LeaguePlayerLink, bool> lplPredicate; 

            if (request.PlayerExternalId is null)
            {
                if (request.Pid is null || request.Pid.Length == 0)
                {
                    return resultLeagues;
                }
                lplPredicate = l => request.Pid.Contains(l.PlayerId);
            }
            else
            {
                var playerInternalIdDto = await _playersService.GetInternalIdAsync(request.PlayerExternalId);
                if (playerInternalIdDto is null)
                {
                    throw new FliGenException(ErrorCodes.NoPlayerWithSuchExternalId, $"There is no player with external id: {request.PlayerExternalId}");
                }
                lplPredicate = l => l.PlayerId == playerInternalIdDto.InternalId;
            }

            foreach (var league in leagues.Items)
            {
                var distinctLinks = league.LeaguePlayerLinks
                    .Where(lplPredicate)
                    .OrderBy(p => p.CreationTime)
                    .GroupBy(p => p.PlayerId)
                    .Select(g => g.Last()).ToList();

                resultLeagues.First(x => x.Id == league.Id).PlayersLeagueStatuses = EnrichByPlayerInformation(distinctLinks);
            }

            return resultLeagues;
        }

        private static List<PlayerWithLeagueStatusDto> EnrichByPlayerInformation(IEnumerable<LeaguePlayerLink> links)
        {
            var list = new List<PlayerWithLeagueStatusDto>();
            foreach (var link in links)
            {
                var playerWithLeagueStatus = new PlayerWithLeagueStatusDto { Id = link.PlayerId };

                if (link.JoinTime == null)
                {
                    playerWithLeagueStatus.PlayerLeagueJoinStatus = PlayerLeagueJoinStatus.Waiting;

                }
                else if (link.LeaveTime == null)
                {
                    playerWithLeagueStatus.PlayerLeagueJoinStatus = PlayerLeagueJoinStatus.Joined;
                }
                else
                {
                    playerWithLeagueStatus.PlayerLeagueJoinStatus = PlayerLeagueJoinStatus.None;
                }

                playerWithLeagueStatus.LeaguePlayerPriority = link.LeaguePlayerPriority;
                list.Add(playerWithLeagueStatus);
            }

            return list;
        }
    }
}
