using FliGen.Domain.Common.Repository;
using FliGen.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
            var playerRepo = _uow.GetRepositoryAsync<Domain.Entities.Player>();
            var lpRepo = _uow.GetRepositoryAsync<LeaguePlayerLink>();

            Domain.Entities.League league = await leagueRepo.SingleAsync(
	            predicate: x => x.Id == request.LeagueId,
	            include: x => x.Include(y => y.LeagueSettings));

            Domain.Entities.Player player = await playerRepo.SingleAsync(
	            predicate: x => x.ExternalId == request.PlayerExternalId,
	            include: x => x.Include(y => y.LeaguePlayerLinks));

            LeaguePlayerLink lastLink = player.LeaguePlayerLinks
	            .Where(z => z.LeagueId == league.Id)
	            .OrderBy(x => x.CreationTime)
	            .Last();

            if (lastLink == null || 
                lastLink.InLeftStatus())
            {
	            LeaguePlayerLink link = league.IsRequireConfirmation()
		            ? LeaguePlayerLink.CreateWaitingLink(league.Id, player.Id)
		            : LeaguePlayerLink.CreateJoinedLink(league.Id, player.Id);

	            await lpRepo.AddAsync(link, cancellationToken);
            }
            else if (lastLink.JoinTime == null)
            {
                if (league.IsRequireConfirmation())
                {
	                lpRepo.RemoveAsync(lastLink);
                }
                else
                {
	                lastLink.UpdateToJoined();
	                lpRepo.UpdateAsync(lastLink);
                }
            }
            else
            {
	            lastLink.UpdateToLeft();
	            lpRepo.UpdateAsync(lastLink);
            }

            _uow.SaveChanges();
            return Unit.Value;
        }
    }
}
