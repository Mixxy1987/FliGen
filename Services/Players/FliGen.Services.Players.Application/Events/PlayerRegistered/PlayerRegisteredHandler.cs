using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FliGen.Common.Handlers;
using FliGen.Common.RabbitMq;
using FliGen.Common.SeedWork.Repository;
using FliGen.Common.SeedWork.Repository.Paging;
using FliGen.Services.Players.Domain.Entities;

namespace FliGen.Services.Players.Application.Events.PlayerRegistered
{
    public class PlayerRegisteredHandler : IEventHandler<PlayerRegistered>
    {
        private readonly IUnitOfWork _uow;

        public PlayerRegisteredHandler(
            IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task HandleAsync(PlayerRegistered @event, ICorrelationContext context)
        {
            var playersRepo = _uow.GetRepositoryAsync<Player>();

            IPaginate<Player> players = await playersRepo.GetListAsync(p =>
                p.FirstName == @event.FirstName &&
                p.LastName == @event.LastName); //todo:: case comparison

            if (players.Count != 0)
            {
                var player = players.Items[0]; //todo:: handle all players
                var updatedPlayer = Player.GetUpdated(player.Id, player.FirstName, player.LastName, @event.ExternalId);
                playersRepo.UpdateAsync(updatedPlayer);
            }
            else
            {
                await playersRepo.AddAsync(Player.Create(@event.FirstName, @event.LastName, @event.ExternalId));
            }

            _uow.SaveChanges();
        }
    }
}