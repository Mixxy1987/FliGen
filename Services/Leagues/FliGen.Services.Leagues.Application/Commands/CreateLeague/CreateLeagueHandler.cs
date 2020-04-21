using FliGen.Common.Handlers;
using FliGen.Common.RabbitMq;
using FliGen.Common.SeedWork;
using FliGen.Common.SeedWork.Repository;
using FliGen.Services.Leagues.Domain.Entities;
using FliGen.Services.Leagues.Domain.Entities.Enum;
using System.Threading.Tasks;
using FliGen.Services.Leagues.Application.Events;

namespace FliGen.Services.Leagues.Application.Commands.CreateLeague
{
    public class CreateLeagueHandler : ICommandHandler<CreateLeague>
    {
        private readonly IUnitOfWork _uow;
        private readonly IBusPublisher _busPublisher;

        public CreateLeagueHandler(
            IUnitOfWork uow,
            IBusPublisher busPublisher)
        {
            _uow = uow;
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(CreateLeague command, ICorrelationContext context)
        {
            var league = League.Create(
                command.Name,
                command.Description,
                Enumeration.FromDisplayName<LeagueType>(command.LeagueType.Name));

            var repo = _uow.GetRepositoryAsync<League>();

            var newLeagueId = (await repo.AddAsync(league)).Entity.Id;

            _uow.SaveChanges();

            await _busPublisher.PublishAsync(new LeagueCreated(newLeagueId), context);
        }
    }
}
