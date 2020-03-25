using FliGen.Common.SeedWork.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FliGen.Application.Commands.Tour.TourCancelCommand
{
    public class TourCancelCommandHandler : IRequestHandler<TourCancelCommand>
    {
        private readonly IUnitOfWork _uow;

        public TourCancelCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Unit> Handle(TourCancelCommand request, CancellationToken cancellationToken)
        {
            var tourRepo = _uow.GetRepositoryAsync<Domain.Entities.Tour>();

            Domain.Entities.Tour tour = await tourRepo.SingleAsync(t => t.Id == request.TourId);
            tour.CancelTour();
            tourRepo.UpdateAsync(tour);

            _uow.SaveChanges();

            return Unit.Value;
        }
    }
}
