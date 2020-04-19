using FliGen.Common.Handlers;
using FliGen.Common.RabbitMq;
using FliGen.Common.SeedWork.Repository;
using FliGen.Services.Players.Domain.Entities;
using FliGen.Services.Players.Domain.Entities.Enum;
using System.Threading.Tasks;

namespace FliGen.Services.Players.Application.Commands.SendMessage
{
    public class SendInboxNotificationHandler : ICommandHandler<SendInboxNotification>
    {
        private readonly IUnitOfWork _uow;

        public SendInboxNotificationHandler(
            IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task HandleAsync(SendInboxNotification command, ICorrelationContext context)
        {
            var message = Message.Create(
                command.Sender,
                command.Topic,
                command.Body);

            var messageRepo = _uow.GetRepositoryAsync<Message>();

            var messageEntity = await messageRepo.AddAsync(message);

            _uow.SaveChanges();

            var playerMessageLinkRepo = _uow.GetRepositoryAsync<PlayerMessageLink>();

            if (command.PlayerIds is null || command.PlayerIds.Length == 0)
            {
                var playerMessageLink = PlayerMessageLink.Create(0, messageEntity.Entity.Id, MessageType.All);
                await playerMessageLinkRepo.AddAsync(playerMessageLink);
            }
            else
            {
                foreach (var playerId in command.PlayerIds)
                {
                    var playerMessageLink = PlayerMessageLink.Create(playerId, messageEntity.Entity.Id, MessageType.Personal);
                    await playerMessageLinkRepo.AddAsync(playerMessageLink);
                }

            }
            _uow.SaveChanges();
        }
    }
}
