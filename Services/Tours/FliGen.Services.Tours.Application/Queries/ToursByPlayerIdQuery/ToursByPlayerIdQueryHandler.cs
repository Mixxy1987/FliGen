﻿using AutoMapper;
using FliGen.Common.SeedWork.Repository;
using FliGen.Common.SeedWork.Repository.Paging;
using FliGen.Services.Tours.Application.Dto;
using FliGen.Services.Tours.Application.Services;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tour = FliGen.Services.Tours.Domain.Entities.Tour;

namespace FliGen.Services.Tours.Application.Queries.ToursByPlayerIdQuery
{
    public class ToursByPlayerIdQueryHandler : IRequestHandler<ToursByPlayerIdQuery, IEnumerable<TourDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ITeamsService _teamsService;

        public ToursByPlayerIdQueryHandler(
            IUnitOfWork uow,
            IMapper mapper,
            ITeamsService teamsService)
        {
            _uow = uow;
            _mapper = mapper;
            _teamsService = teamsService;
        }

        public async Task<IEnumerable<TourDto>> Handle(ToursByPlayerIdQuery request, CancellationToken cancellationToken)
        {
            ToursByPlayerIdDto toursByPlayerIdDto = await _teamsService.GetToursByPlayerIdAsync(request.Size, request.Page, request.PlayerId);

            if (toursByPlayerIdDto is null)
            {
                return null;
            }

            List<int> tourIds = toursByPlayerIdDto.ToursId.ToList();

            var toursRepo = _uow.GetReadOnlyRepository<Tour>();

            IPaginate<Tour> tours = toursRepo.GetList(
                t => tourIds.Contains(t.Id),
                size: tourIds.Count);

            var toursDtos = new List<TourDto>();

            foreach (var tour in tours.Items)
            {
                if (request.QueryType == ToursByPlayerIdQueryType.Incoming && tour.IsEnded())
                { // we want incoming tour, but this tour is ended - continue
                    continue;
                }
                if (request.SeasonIds != null && request.SeasonIds.Length != 0 && !request.SeasonIds.Contains(tour.SeasonId))
                { // we want tours for specific seasons, but this tour is from another season - continue
                    continue;
                }
                toursDtos.Add(_mapper.Map<TourDto>(tour));
            }

            return toursDtos;
        }
    }
}