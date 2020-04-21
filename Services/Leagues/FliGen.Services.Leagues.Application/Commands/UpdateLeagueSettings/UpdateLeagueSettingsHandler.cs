using FliGen.Common.Handlers;
using FliGen.Common.RabbitMq;
using FliGen.Common.SeedWork.Repository;
using FliGen.Common.Types;
using FliGen.Services.Leagues.Application.Events;
using FliGen.Services.Leagues.Domain.Common;
using System.Threading.Tasks;

namespace FliGen.Services.Leagues.Application.Commands.UpdateLeagueSettings
{
    public class UpdateLeagueSettingsHandler : ICommandHandler<UpdateLeagueSettings>
    {
        private readonly IUnitOfWork _uow;
        private readonly IBusPublisher _busPublisher;

        public UpdateLeagueSettingsHandler(
            IUnitOfWork uow,
            IBusPublisher busPublisher)
        {
            _uow = uow;
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(UpdateLeagueSettings command, ICorrelationContext context)
        {
            var repo = _uow.GetRepositoryAsync<Domain.Entities.LeagueSettings>();

            Domain.Entities.LeagueSettings leagueSettings = await repo.SingleAsync(x => x.LeagueId == command.LeagueId);
            
            if (leagueSettings is null)
            {
                throw new FliGenException(ErrorCodes.NoLeagueSettings, $"There is no league settings for league: {command.LeagueId}");
            }

            repo.UpdateAsync(Domain.Entities.LeagueSettings.GetUpdated(
                leagueSettings,
                command.Visibility,
                command.RequireConfirmation,
                command.PlayersInTeam,
                command.TeamsInTour));

            _uow.SaveChanges();
            await _busPublisher.PublishAsync(new LeagueSettingsUpdated(command.LeagueId), context);
        }
    }
}
