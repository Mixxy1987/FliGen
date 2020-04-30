using AutoMapper;
using FliGen.Common.SeedWork.Repository;
using FliGen.Services.Tours.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FliGen.Services.Tours.Application.Queries.TourById
{
    public class TourByIdQueryHandler : IRequestHandler<TourByIdQuery, Dto.TourDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public TourByIdQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public Task<Dto.TourDto> Handle(TourByIdQuery request, CancellationToken cancellationToken)
        {
            var toursRepo = _uow.GetReadOnlyRepository<Tour>();
            var tour = toursRepo.Single(t => t.Id == request.TourId);
            if (tour is null)
            {
                return null;
            }

            return Task.FromResult(_mapper.Map<Dto.TourDto>(tour));
        }
    }
}