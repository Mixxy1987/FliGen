using System.Threading.Tasks;
using FliGen.Common.Handlers;
using FliGen.Common.RabbitMq;
using FliGen.Common.SeedWork.Repository;
using FliGen.Services.Tours.Application.Events;
using FliGen.Services.Tours.Domain.Entities;


namespace FliGen.Services.Tours.Application.Commands.TourBack
{
    public class TourBackHandler : ICommandHandler<TourBack>
    {
        private readonly IUnitOfWork _uow;
        private readonly IBusPublisher _busPublisher;

        public TourBackHandler(
            IUnitOfWork uow,
            IBusPublisher busPublisher)
        {
            _uow = uow;
            _busPublisher = busPublisher;
        }

        public async Task HandleAsync(TourBack command, ICorrelationContext context)
        {
            var tourRepo = _uow.GetRepositoryAsync<Tour>();
            
            Tour tour = await tourRepo.SingleAsync(t => t.Id == command.TourId);
            tour.MoveTourStatusBack();

            tourRepo.UpdateAsync(tour);

            _uow.SaveChanges();
        }
    }
}
