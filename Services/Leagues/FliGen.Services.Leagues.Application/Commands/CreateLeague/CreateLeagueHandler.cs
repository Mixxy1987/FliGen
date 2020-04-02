using FliGen.Common.Handlers;
using FliGen.Common.RabbitMq;
using FliGen.Common.SeedWork;
using FliGen.Common.SeedWork.Repository;
using FliGen.Services.Leagues.Domain.Entities;
using FliGen.Services.Leagues.Domain.Entities.Enum;
using System.Threading.Tasks;

namespace FliGen.Services.Leagues.Application.Commands.CreateLeague
{
    public class CreateLeagueHandler : ICommandHandler<CreateLeague>
    {
        private readonly IUnitOfWork _uow;

        public CreateLeagueHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task HandleAsync(CreateLeague command, ICorrelationContext context)
        {
            var league = League.Create(
                command.Name,
                command.Description,
                Enumeration.FromDisplayName<LeagueType>(command.LeagueType.Name));

            var repo = _uow.GetRepositoryAsync<League>();

            await repo.AddAsync(league);

            _uow.SaveChanges();
        }
    }
}
