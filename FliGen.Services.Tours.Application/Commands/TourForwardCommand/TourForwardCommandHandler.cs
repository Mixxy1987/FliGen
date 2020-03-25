using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace FliGen.Services.Tours.Application.Commands.TourForwardCommand
{
    public class TourForwardCommandHandler : IRequestHandler<TourForwardCommand>
    {
        //private readonly IUnitOfWork _uow;

        public TourForwardCommandHandler(/*IUnitOfWork uow*/)
        {
	        //_uow = uow;
        }

        public async Task<Unit> Handle(TourForwardCommand request, CancellationToken cancellationToken)
        {
            /*var tourRepo = _uow.GetRepositoryAsync<Domain.Entities.Tour>();

            if (request.TourId is null)
            {
                var newTour = Domain.Entities.Tour.Create(request.Date, request.SeasonId);
                await tourRepo.AddAsync(newTour, cancellationToken);
            }
            else
            {
                Domain.Entities.Tour tour = await tourRepo.SingleAsync(t => t.Id == request.TourId);
                tour.MoveTourStatusForward();
                tourRepo.UpdateAsync(tour);
            }

            _uow.SaveChanges();*/

            return Unit.Value;
        }
    }
}
