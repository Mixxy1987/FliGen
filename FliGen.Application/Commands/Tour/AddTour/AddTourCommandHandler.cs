using FliGen.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FliGen.Application.Commands.Tour.AddTour
{
    /*public class AddTourCommandHandler : IRequestHandler<AddTourCommand>
    {
        private readonly ITourRepository _repository;

        public AddTourCommandHandler(ITourRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(AddTourCommand request, CancellationToken cancellationToken)
        {
            var tour = Tour.Create(request.Date);
            
            await _repository.AddAsync(tour);
            
            return Unit.Value;
        }
    }*/
}
