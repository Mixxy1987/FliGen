using FliGen.Common.SeedWork.Repository;
using FliGen.Services.Leagues.Domain.Common;
using FliGen.Services.Leagues.Domain.Entities;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FliGen.Services.Leagues.Application.Queries.PlayerJoinedLeagues
{
    public class PlayerJoinedLeaguesHandler : IRequestHandler<PlayerJoinedLeagues, int[]>
    {
        private readonly IUnitOfWork _uow;

        public PlayerJoinedLeaguesHandler(
            IUnitOfWork uow)
        {
	        _uow = uow;
        }

        public async Task<int[]> Handle(PlayerJoinedLeagues request, CancellationToken cancellationToken)
        {
            var lplRepo = _uow.GetReadOnlyRepository<LeaguePlayerLink>();

            var links = lplRepo.GetList(
                l => l.PlayerId == request.PlayerId &&
                     l.JoinTime != null &&
                     l.LeaveTime == null, //todo:: how to do it better?
                size: Consts.PlayerCanJoinLeaguesMax).Items;

            if (links.Count == 0)
            {
                return Array.Empty<int>();
            }

            return links.Select(l => l.LeagueId).ToArray();
        }
    }
}
