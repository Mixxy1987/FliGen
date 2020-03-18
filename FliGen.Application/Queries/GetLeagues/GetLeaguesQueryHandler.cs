using FliGen.Domain.Common.Repository;
using FliGen.Domain.Common.Repository.Paging;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FliGen.Application.Dto;
using Microsoft.EntityFrameworkCore;
using League = FliGen.Domain.Entities.League;
using Player = FliGen.Domain.Entities.Player;

namespace FliGen.Application.Queries.GetLeagues
{
    public class GetLeaguesQueryHandler : IRequestHandler<GetLeaguesQuery, IEnumerable<Dto.League>>
    {
        private readonly IUnitOfWork _uow;

        public GetLeaguesQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<Dto.League>> Handle(GetLeaguesQuery request, CancellationToken cancellationToken)
        {
            var leagueRepo = _uow.GetRepositoryAsync<League>();

            IPaginate<League> leagues = await leagueRepo.GetListAsync(cancellationToken : cancellationToken);

            var resultLeagues = leagues.Items.Select(x => new Dto.League() //todo:: paging
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                LeagueType = new LeagueType()
                {
                    Name = x.Type.Name
                }
            }).ToList(); //todo:: automapper?

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

                var leagueStatus = new Dictionary<int, PlayerLeagueJoinStatus>();

                foreach (var links in player.LeaguePlayerLinks.OrderByDescending(x => x.JoinTime))
                {
                    if (leagueStatus.ContainsKey(links.LeagueId)) continue;

                    if (links.JoinTime == null)
                    {
                        leagueStatus[links.LeagueId] = PlayerLeagueJoinStatus.Waiting;
                    }
                    else if (links.LeaveTime == null)
                    {
                        leagueStatus[links.LeagueId] = PlayerLeagueJoinStatus.Joined;
                    }
                }

                foreach (var league in leagueStatus)
                {
                    resultLeagues.Single(x => x.Id == league.Key).PlayerLeagueJoinStatus = league.Value;
                }
            }

            return resultLeagues;
        }
    }
}
