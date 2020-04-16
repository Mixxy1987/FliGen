using FliGen.Common.Handlers;
using FliGen.Common.RabbitMq;
using FliGen.Common.SeedWork.Repository;
using System.Threading.Tasks;

namespace FliGen.Services.Players.Application.Commands.AddPlayer
{
    public class AddPlayerHandler : ICommandHandler<AddPlayer>
    {
        private readonly IUnitOfWork _uow;

        public AddPlayerHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task HandleAsync(AddPlayer command, ICorrelationContext context)
        {
            var player = Domain.Entities.Player.Create(
                command.FirstName,
                command.LastName,
                rate: command.Rate);

            var repo = _uow.GetRepositoryAsync<Domain.Entities.Player>();

            await repo.AddAsync(player);

            _uow.SaveChanges();
        }
    }
}
