using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using FliGen.Domain.Common.Repository;
using FliGen.Domain.Entities;
using FliGen.Domain.Entities.Enum;

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

            Domain.Entities.League league = await leagueRepo.SingleAsync(predicate: x => x.Id == request.LeagueId);

            var playerRepo = _uow.GetRepositoryAsync<Domain.Entities.Player>();

            Domain.Entities.Player player = await playerRepo.SingleAsync(predicate: x => x.ExternalId == request.PlayerExternalId);

            var lpRepo = _uow.GetRepositoryAsync<LeaguePlayerLink>();

            var link = new LeaguePlayerLink()
            {
                LeagueId = league.Id,
                PlayerId = player.Id,
                JoinTime = DateTime.Now,
                LeaguePlayerRoleId = LeaguePlayerRole.User.Id
            };

            await lpRepo.AddAsync(link, cancellationToken);
          
            var result = _uow.SaveChanges();
            return Unit.Value;
        }
    }
}
