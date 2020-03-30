using AutoMapper;
using FliGen.Common.SeedWork.Repository;
using FliGen.Common.SeedWork.Repository.Paging;
using FliGen.Services.Players.Application.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FliGen.Services.Players.Application.Queries.Players
{
    public class PlayersQueryHandler: IRequestHandler<PlayersQuery, IEnumerable<PlayerWithRate>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public PlayersQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PlayerWithRate>> Handle(PlayersQuery request, CancellationToken cancellationToken)
        {
            var repo = _uow.GetRepositoryAsync<Domain.Entities.Player>();

            IPaginate<Domain.Entities.Player> players = await repo.GetListAsync(
                include: source => source.Include(a => a.Rates),
                size: request.Size,
                cancellationToken: cancellationToken);

            if (players.Count == 0)
            {
                return null;
            }

            var playersWithRates = new List<PlayerWithRate>();

            foreach (var player in players.Items)
            {
                var list = new List<PlayerLeagueRate>();

                var rates = request.QueryType == PlayersQueryType.Actual ?
                    player.Rates
                        .OrderBy(p => p.Date)
                        .GroupBy(p => p.LeagueId)
                        .Select(g => g.Last()) :
                    player.Rates;

                foreach (var rate in rates)
                {
                    if (request.LeagueIds.Length != 0 && !request.LeagueIds.Contains(rate.LeagueId))
                    {
                        continue;
                    }
                    list.Add(_mapper.Map<PlayerLeagueRate>(rate));
                }

                if (list.Count != 0)
                {
                    playersWithRates.Add(
                        new PlayerWithRate()
                        {
                            Id = player.Id,
                            FirstName = player.FirstName,
                            LastName = player.LastName,
                            PlayerLeagueRates = list
                        });
                }
            }

            return playersWithRates;
        }
    }
}
