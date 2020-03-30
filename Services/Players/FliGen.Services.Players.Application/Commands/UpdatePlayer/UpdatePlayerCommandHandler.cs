using FliGen.Common.SeedWork.Repository;
using FliGen.Services.Players.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FliGen.Services.Players.Application.Commands.UpdatePlayer
{
    public class UpdatePlayerCommandHandler : IRequestHandler<UpdatePlayerCommand>
    {
        private readonly IUnitOfWork _uow;

        public UpdatePlayerCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Unit> Handle(UpdatePlayerCommand request, CancellationToken cancellationToken)
        {
            var playerRepo = _uow.GetRepositoryAsync<Player>();

            Player player = await playerRepo.SingleAsync(
                predicate: x => x.Id == request.Id,
                include: source => source.Include(a => a.Rates));

            var playerRate = player.Rates
                .Where(r => r.LeagueId == request.LeagueId)
                .OrderBy(y => y.Date)
                .LastOrDefault();

            if (playerRate == null || playerRate.Value.ToString("F2") != request.Rate)
            {
                var newPlayerRate = new PlayerRate(DateTime.UtcNow, request.Rate, player.Id, request.LeagueId);
                var playerRatesRepo = _uow.GetRepositoryAsync<PlayerRate>();
                await playerRatesRepo.AddAsync(newPlayerRate, cancellationToken);
            }

            if (!string.IsNullOrWhiteSpace(request.FirstName) &&
                !string.IsNullOrWhiteSpace(request.LastName))
            {
                playerRepo.UpdateAsync(Player.GetUpdated(
                    player.Id, request.FirstName, request.LastName, player.ExternalId));
            }
          
            _uow.SaveChanges();

            return Unit.Value;
        }
    }
}
