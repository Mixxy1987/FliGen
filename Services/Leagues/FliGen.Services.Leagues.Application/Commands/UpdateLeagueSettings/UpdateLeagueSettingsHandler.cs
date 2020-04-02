using System.Threading;
using System.Threading.Tasks;
using FliGen.Common.SeedWork.Repository;
using MediatR;

namespace FliGen.Services.Leagues.Application.Commands.UpdateLeagueSettings
{
    public class UpdateLeagueSettingsCommandHandler : IRequestHandler<UpdateLeagueSettingsCommand>
    {
        private readonly IUnitOfWork _uow;

        public UpdateLeagueSettingsCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Unit> Handle(UpdateLeagueSettingsCommand request, CancellationToken cancellationToken)
        {
            var repo = _uow.GetRepositoryAsync<Domain.Entities.LeagueSettings>();

            Domain.Entities.LeagueSettings leagueSetting = await repo.SingleAsync(x => x.LeagueId == request.LeagueId);

            repo.UpdateAsync(Domain.Entities.LeagueSettings.GetUpdated(
                leagueSetting,
                request.Visibility,
                request.RequireConfirmation));

            _uow.SaveChanges();

            return Unit.Value;
        }
    }
}
