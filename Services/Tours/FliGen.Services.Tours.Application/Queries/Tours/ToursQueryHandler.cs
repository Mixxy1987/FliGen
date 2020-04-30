using AutoMapper;
using FliGen.Common.SeedWork.Repository;
using FliGen.Services.Tours.Application.Dto;
using FliGen.Services.Tours.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using FliGen.Common.Extensions;
using FliGen.Common.SeedWork.Repository.Paging;

namespace FliGen.Services.Tours.Application.Queries.Tours
{
    public class ToursQueryHandler : IRequestHandler<ToursQuery, IEnumerable<TourDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ToursQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TourDto>> Handle(ToursQuery request, CancellationToken cancellationToken)
        {
            var toursRepo = _uow.GetReadOnlyRepository<Tour>();
            //todo::
            /*Expression<Func<Tour, bool>> predicate = null;
            if (!(request.SeasonsId is null) && request.SeasonsId.Length != 0)
            {
                predicate = tour => request.SeasonsId.Contains(tour.SeasonId);
            }

            var toursDto = new List<TourDto>();
            IList<Tour> tours = new List<Tour>();
            if (request.ToursQueryType == ToursQueryType.All)
            {
                tours = toursRepo.GetList(predicate).Items;
            }
            else if (request.ToursQueryType == ToursQueryType.Last)
            {
                IPaginate<Tour> t = toursRepo.GetList(
                    predicate: predicate,
                    orderBy: q => q.OrderByDescending(t => t.Date), 
                    size : request.Last);

                tours = t.Items;
            }

            foreach (var tour in tours)
            {
                toursDto.Add(_mapper.Map<TourDto>(tours));
            }

            return toursDto;*/
            return null;
        }
    }
}