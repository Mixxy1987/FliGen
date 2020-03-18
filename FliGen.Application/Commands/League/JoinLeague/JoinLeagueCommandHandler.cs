using System;
using System.Linq;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using FliGen.Domain.Common.Repository;
using FliGen.Domain.Entities;
using FliGen.Domain.Entities.Enum;
using Microsoft.EntityFrameworkCore;

namespace FliGen.Application.Commands.League.JoinLeague
{
    public class JoinLeagueCommandHandler : IRequestHandler<JoinLeagueCommand>
    {
        private readonly IUnitOfWork _uow;

        public JoinLeagueCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Unit> Handle(JoinLeagueCommand request, CancellationToken cancellationToken)
        {
            var leagueRepo = _uow.GetRepositoryAsync<Domain.Entities.League>();

            Domain.Entities.League league = await leagueRepo.SingleAsync(
	            predicate: x => x.Id == request.LeagueId,
	            include: x => x.Include(y => y.LeagueSettings));

            var playerRepo = _uow.GetRepositoryAsync<Domain.Entities.Player>();

            Domain.Entities.Player player = await playerRepo.SingleAsync(
	            predicate: x => x.ExternalId == request.PlayerExternalId,
	            include: x => x.Include(y => y.LeaguePlayerLinks.Where(z => z.LeagueId == league.Id)));

            var lpRepo = _uow.GetRepositoryAsync<LeaguePlayerLink>();

            var lastLink = player.LeaguePlayerLinks.OrderBy(x => x.CreationTime).Last(); //todo:: check

            if (lastLink.JoinTime != null && lastLink.LeaveTime != null)
            {
	            LeaguePlayerLink link = league.IsRequireConfirmation()
		            ? LeaguePlayerLink.CreateWaitingLink(league.Id, player.Id)
		            : LeaguePlayerLink.CreateJoinedLink(league.Id, player.Id);

	            await lpRepo.AddAsync(link, cancellationToken);
            }
            else
            {
	            LeaguePlayerLink link = lastLink.JoinTime == null ?
		            LeaguePlayerLink.UpdateToJoinedLink(lastLink) :
		            LeaguePlayerLink.UpdateToLeftLink(lastLink);
	            lpRepo.UpdateAsync(link);
            }

            var result = _uow.SaveChanges();
            return Unit.Value;
        }
    }
}
