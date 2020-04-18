using FliGen.Common.Handlers;
using FliGen.Common.RabbitMq;
using FliGen.Common.SeedWork.Repository;
using System.Threading.Tasks;
using FliGen.Services.Players.Domain.Entities;
using FliGen.Services.Players.Domain.Entities.Enum;

namespace FliGen.Services.Players.Application.Commands.SendMessage
{
    public class SendMessageHandler : ICommandHandler<SendMessage>
    {
        private readonly IUnitOfWork _uow;
        private readonly IBusPublisher _busPublisher;

        public SendMessageHandler(
            IUnitOfWork uow,
            IBusPublisher busPublisher)
        {
            _uow = uow;
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(SendMessage command, ICorrelationContext context)
        {
            var message = Message.Create(
                command.From,
                command.Topic,
                command.Body);

            var messageRepo = _uow.GetRepositoryAsync<Message>();

            var messageEntity = await messageRepo.AddAsync(message);

            _uow.SaveChanges();

            int playerId;
            int messageTypeId;
            if (command.PlayerId is null || command.PlayerId <= 0)
            {
                playerId = 0;
                messageTypeId = MessageType.All;
            }
            else
            {
                playerId = (int)command.PlayerId;
                messageTypeId = MessageType.Personal;
            }
            var playerMessageLinkRepo = _uow.GetRepositoryAsync<PlayerMessageLink>();
            var playerMessageLink = PlayerMessageLink.Create(playerId, messageEntity.Entity.Id, messageTypeId);

            await playerMessageLinkRepo.AddAsync(playerMessageLink);

            _uow.SaveChanges();
        }
    }
}
