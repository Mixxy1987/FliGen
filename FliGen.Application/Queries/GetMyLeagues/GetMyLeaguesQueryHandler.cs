using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FliGen.Application.CommonLogic;
using FliGen.Domain.Common.Repository;
using FliGen.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FliGen.Application.Queries.GetMyLeagues
{
    public class GetMyLeaguesQueryHandler : IRequestHandler<GetMyLeaguesQuery, IEnumerable<Dto.League>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetMyLeaguesQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
	        _uow = uow;
	        _mapper = mapper;
        }

        public async Task<IEnumerable<Dto.League>> Handle(GetMyLeaguesQuery request, CancellationToken cancellationToken)
        {
            var playerRepo = _uow.GetRepositoryAsync<Player>();
            var leagueRepo = _uow.GetRepositoryAsync<Domain.Entities.League>();

            Player player = await playerRepo.SingleAsync(
                predicate: x => x.ExternalId == request.UserId,
                include: source => source.Include(a => a.LeaguePlayerLinks));

            List<LeaguePlayerLink> distinctLinks = player.LeaguePlayerLinks
                 .OrderBy(p => p.CreationTime)
                 .Where(l => !l.InLeftStatus())
                 .GroupBy(p => p.LeagueId)
                 .Select(g => g.Last())
                 .ToList();

            var leagues = new List<Domain.Entities.League>();

            foreach (var link in distinctLinks)
            {
                leagues.Add(await leagueRepo.SingleAsync(x => x.Id == link.LeagueId));
            }

            List<Dto.League> resultLeagues = leagues
                .Select(x => _mapper.Map<Dto.League>(x))
	            .ToList();

            resultLeagues.EnrichByPlayerLeagueJoinStatus(distinctLinks);

            return resultLeagues;
        }
    }
}