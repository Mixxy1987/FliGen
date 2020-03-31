using AutoMapper;
using FliGen.Common.SeedWork.Repository;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FliGen.Common.SeedWork.Repository.Paging;
using FliGen.Services.Tours.Application.Dto;
using FliGen.Services.Tours.Application.Services;
using Tour = FliGen.Services.Tours.Domain.Entities.Tour;

namespace FliGen.Services.Tours.Application.Queries.MyTours
{
    public class MyToursQueryHandler : IRequestHandler<MyToursQuery, IEnumerable<Dto.Tour>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ITeamsService _teamsService;

        public MyToursQueryHandler(
            IUnitOfWork uow,
            IMapper mapper,
            ITeamsService teamsService)
        {
            _uow = uow;
            _mapper = mapper;
            _teamsService = teamsService;
        }

        public async Task<IEnumerable<Dto.Tour>> Handle(MyToursQuery request, CancellationToken cancellationToken)
        {
            ToursByPlayerIdDto toursByPlayerIdDto = await _teamsService.GetToursByPlayerIdAsync(request.PlayerId, request.Size);

            if (toursByPlayerIdDto is null)
            {
                return null;
            }

            List<int> tourIds = toursByPlayerIdDto.TourDtos.Select(x => x.TourId).ToList();

            var toursRepo = _uow.GetReadOnlyRepository<Tour>();

            IPaginate<Tour> tours = toursRepo.GetList(
                t => tourIds.Contains(t.Id),
                size: tourIds.Count);

            var toursDtos = new List<Dto.Tour>();

            foreach (var tour in tours.Items)
            {
                if (request.QueryType == MyToursQueryType.Incoming && tour.IsEnded())
                { // we want incoming tour, but this tour is ended - continue
                    continue;
                }
                if (request.SeasonIds.Length != 0 && !request.SeasonIds.Contains(tour.SeasonId))
                { // we want tours for specific seasons, but this tour is from another season - continue
                    continue;
                }
                toursDtos.Add(_mapper.Map<Dto.Tour>(tour));
            }

            return toursDtos;
        }
    }
}