using FliGen.Common.Handlers;
using FliGen.Common.RabbitMq;
using FliGen.Common.SeedWork.Repository;
using System.Threading.Tasks;

namespace FliGen.Services.Tours.Application.Commands.TourForward
{
    public class TourForwardHandler : ICommandHandler<TourForward>
    {
        private readonly IUnitOfWork _uow;

        public TourForwardHandler(IUnitOfWork uow)
        {
	        _uow = uow;
        }

        public async Task HandleAsync(TourForward command, ICorrelationContext context)
        {
            var tourRepo = _uow.GetRepositoryAsync<Domain.Entities.Tour>();

            if (command.TourId is null)
            {
                var newTour = Domain.Entities.Tour.Create(command.Date, command.SeasonId);
                await tourRepo.AddAsync(newTour);
            }
            else
            {
                Domain.Entities.Tour tour = await tourRepo.SingleAsync(t => t.Id == command.TourId);
                tour.MoveTourStatusForward();
                tourRepo.UpdateAsync(tour);
            }

            _uow.SaveChanges();
        }
    }
}
