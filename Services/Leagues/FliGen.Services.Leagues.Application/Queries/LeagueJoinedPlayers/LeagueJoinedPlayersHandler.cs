using FliGen.Common.SeedWork.Repository;
using FliGen.Common.Types;
using FliGen.Services.Leagues.Application.Common;
using FliGen.Services.Leagues.Application.Dto;
using FliGen.Services.Leagues.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FliGen.Services.Leagues.Application.Queries.LeagueJoinedPlayers
{
    public class LeagueJoinedPlayersHandler : IRequestHandler<LeagueJoinedPlayers, IEnumerable<PlayerInternalIdDto>>
    {
        private readonly IUnitOfWork _uow;

        public LeagueJoinedPlayersHandler(
            IUnitOfWork uow)
        {
	        _uow = uow;
        }

        public async Task<IEnumerable<PlayerInternalIdDto>> Handle(LeagueJoinedPlayers request, CancellationToken cancellationToken)
        {
            var leagueRepo = _uow.GetRepositoryAsync<League>();

            League league = await leagueRepo
                .SingleAsync(
                    predicate: l => l.Id == request.LeagueId,
                    include: q => q.Include(l => l.LeaguePlayerLinks)); 

            if (league is null)
            {
                throw new FliGenException(
                    ErrorCodes.NoLeagueWithSuchId,
                    $"There is no league with id: {request.LeagueId}");
            }

            return league.LeaguePlayerLinks
                .Where(lpl => lpl.InJoinedStatus())
                .Select(lpl => new PlayerInternalIdDto() {InternalId = lpl.PlayerId});
        }
    }
}
