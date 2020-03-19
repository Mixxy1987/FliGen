using System;
using FliGen.Domain.Common.Repository;
using FliGen.Domain.Common.Repository.Paging;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FliGen.Application.CommonLogic;
using FliGen.Application.Dto;
using FliGen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using League = FliGen.Domain.Entities.League;
using Player = FliGen.Domain.Entities.Player;
using AutoMapper;

namespace FliGen.Application.Queries.GetLeagues
{
    public class GetLeaguesQueryHandler : IRequestHandler<GetLeaguesQuery, IEnumerable<Dto.League>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetLeaguesQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
	        _uow = uow;
	        _mapper = mapper;
        }

        public async Task<IEnumerable<Dto.League>> Handle(GetLeaguesQuery request, CancellationToken cancellationToken)
        {
            var leagueRepo = _uow.GetRepositoryAsync<League>();

            IPaginate<League> leagues = await leagueRepo.GetListAsync(cancellationToken : cancellationToken);


            List<Dto.League> resultLeagues = leagues.Items
	            .Select(x => _mapper.Map<Dto.League>(x))
	            .ToList();

            if (request.UserId != null)
            {
                var playerRepo = _uow.GetRepositoryAsync<Player>();

                Player player = await playerRepo.SingleAsync(
                    predicate: x => x.ExternalId == request.UserId,
                    include: source => source.Include(a => a.LeaguePlayerLinks));

                if (player == null)
                {
                    return resultLeagues;
                }

                IEnumerable<LeaguePlayerLink> distinctLinks = player.LeaguePlayerLinks
	                .OrderBy(p => p.CreationTime)
	                .GroupBy(p => p.LeagueId)
	                .Select(g => g.Last());

                resultLeagues.EnrichByPlayerLeagueJoinStatus(distinctLinks);
            }

            return resultLeagues;
        }
    }
}
