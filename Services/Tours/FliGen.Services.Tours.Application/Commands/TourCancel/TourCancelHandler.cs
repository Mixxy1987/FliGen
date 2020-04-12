using FliGen.Common.Handlers;
using FliGen.Common.RabbitMq;
using FliGen.Common.SeedWork.Repository;
using FliGen.Services.Tours.Application.Events;
using FliGen.Services.Tours.Domain.Entities;
using System.Threading.Tasks;

namespace FliGen.Services.Tours.Application.Commands.TourCancel
{
    public class TourCancelHandler : ICommandHandler<TourCancel>
    {
        private readonly IUnitOfWork _uow;
        private readonly IBusPublisher _busPublisher;

        public TourCancelHandler(
            IUnitOfWork uow,
            IBusPublisher busPublisher)
        {
            _uow = uow;
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(TourCancel command, ICorrelationContext context)
        {
            var tourRepo = _uow.GetRepositoryAsync<Tour>();

            Tour tour = await tourRepo.SingleAsync(t => t.Id == command.TourId);
            tour.CancelTour();
            tourRepo.UpdateAsync(tour);
            await _busPublisher.PublishAsync(new TourCanceled(tour.Id), context);
            
            _uow.SaveChanges();
        }
    }
}
