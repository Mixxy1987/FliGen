using FliGen.Common.Handlers;
using FliGen.Common.RabbitMq;
using FliGen.Common.SeedWork.Repository;
using FliGen.Services.Players.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using FliGen.Services.Players.Application.Events;

namespace FliGen.Services.Players.Application.Commands.UpdatePlayer
{
    public class UpdatePlayerHandler : ICommandHandler<UpdatePlayer>
    {
        private readonly IUnitOfWork _uow;
        private readonly IBusPublisher _busPublisher;

        public UpdatePlayerHandler(
            IUnitOfWork uow,
            IBusPublisher busPublisher)
        {
            _uow = uow;
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(UpdatePlayer command, ICorrelationContext context)
        {
            var playerRepo = _uow.GetRepositoryAsync<Player>();

            Player player = await playerRepo.SingleAsync(
                predicate: x => x.Id == command.Id,
                include: source => source.Include(a => a.Rates));

            var playerRate = player.Rates
                .Where(r => r.LeagueId == command.LeagueId)
                .OrderBy(y => y.Date)
                .LastOrDefault();

            if (playerRate == null || playerRate.Value.ToString("F2") != command.Rate)
            {
                var newPlayerRate = new PlayerRate(DateTime.UtcNow, command.Rate, player.Id, command.LeagueId);
                var playerRatesRepo = _uow.GetRepositoryAsync<PlayerRate>();
                await playerRatesRepo.AddAsync(newPlayerRate);
            }

            if (!string.IsNullOrWhiteSpace(command.FirstName) &&
                !string.IsNullOrWhiteSpace(command.LastName))
            {
                playerRepo.UpdateAsync(Player.GetUpdated(
                    player.Id, command.FirstName, command.LastName, player.ExternalId));
            }
          
            _uow.SaveChanges();
            await _busPublisher.PublishAsync(new PlayerUpdated(), context);
        }
    }
}
