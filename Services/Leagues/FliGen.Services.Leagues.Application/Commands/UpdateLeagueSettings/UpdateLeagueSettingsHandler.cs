using FliGen.Common.Handlers;
using FliGen.Common.RabbitMq;
using FliGen.Common.SeedWork.Repository;
using System.Threading.Tasks;

namespace FliGen.Services.Leagues.Application.Commands.UpdateLeagueSettings
{
    public class UpdateLeagueSettingsHandler : ICommandHandler<UpdateLeagueSettings>
    {
        private readonly IUnitOfWork _uow;

        public UpdateLeagueSettingsHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task HandleAsync(UpdateLeagueSettings command, ICorrelationContext context)
        {
            var repo = _uow.GetRepositoryAsync<Domain.Entities.LeagueSettings>();

            Domain.Entities.LeagueSettings leagueSetting = await repo.SingleAsync(x => x.LeagueId == command.LeagueId);

            repo.UpdateAsync(Domain.Entities.LeagueSettings.GetUpdated(
                leagueSetting,
                command.Visibility,
                command.RequireConfirmation,
                command.PlayersInTeam,
                command.TeamsInTour));

            _uow.SaveChanges();
        }
    }
}
