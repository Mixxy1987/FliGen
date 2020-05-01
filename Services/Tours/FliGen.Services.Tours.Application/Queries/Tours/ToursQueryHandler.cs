using AutoMapper;
using FliGen.Common.Extensions;
using FliGen.Common.SeedWork.Repository;
using FliGen.Common.SeedWork.Repository.Paging;
using FliGen.Services.Tours.Application.Dto;
using FliGen.Services.Tours.Application.Services;
using FliGen.Services.Tours.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace FliGen.Services.Tours.Application.Queries.Tours
{
    public class ToursQueryHandler : IRequestHandler<ToursQuery, IEnumerable<TourDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ITeamsService _teamsService;

        public ToursQueryHandler(
            IUnitOfWork uow,
            IMapper mapper,
            ITeamsService teamsService)
        {
            _uow = uow;
            _mapper = mapper;
            _teamsService = teamsService;
        }

        public async Task<IEnumerable<TourDto>> Handle(ToursQuery request, CancellationToken cancellationToken)
        {
            var toursRepo = _uow.GetReadOnlyRepository<Tour>();

            Expression<Func<Tour, bool>> predicate = null;
            if (!(request.SeasonsId is null) && request.SeasonsId.Length != 0)
            {
                predicate = tour => request.SeasonsId.Contains(tour.SeasonId);
            }

            IPaginate<Tour> tours;
            if (request.PlayerId == 0)
            {
                tours = QueryForAllPlayers(request, toursRepo, predicate);
            }
            else
            {
                tours = await FilterByPlayerId(request, toursRepo, predicate);
            }
            
            var toursDtos = new List<TourDto>();

            foreach (var tour in tours.Items)
            {
                if (request.QueryType == ToursQueryType.Incoming && tour.IsEnded())
                { // we want incoming tour, but this tour is ended - continue
                    continue;
                }

                toursDtos.Add(_mapper.Map<TourDto>(tour));
            }

            return toursDtos;
        }

        private async Task<IPaginate<Tour>> FilterByPlayerId(
            ToursQuery request,
            IRepositoryReadOnly<Tour> toursRepo,
            Expression<Func<Tour, bool>> predicate)
        {
            ToursByPlayerIdDto toursByPlayerIdDto =
                await _teamsService.GetToursByPlayerIdAsync(
                    request.Size,
                    request.Page,
                    request.PlayerId);

            if (toursByPlayerIdDto?.ToursId is null ||
                toursByPlayerIdDto.ToursId.Length == 0)
            {
                return null;
            }
            var tourIds = toursByPlayerIdDto.ToursId.ToList();

            return toursRepo.GetList(
                predicate: predicate.AndAlso(t => tourIds.Contains(t.Id)),
                size: tourIds.Count);
        }

        private IPaginate<Tour> QueryForAllPlayers(
            ToursQuery request,
            IRepositoryReadOnly<Tour> toursRepo,
            Expression<Func<Tour, bool>> predicate)
        {
            if (request.QueryType == ToursQueryType.All)
            {
                return toursRepo.GetList(predicate);
            }

            int size = request.QueryType == ToursQueryType.Last ? request.Last : 1;

            if (request.SeasonsId != null &&
                request.SeasonsId.Length != 0)
            {
                var tours = new List<Tour>();

                foreach (var seasonId in request.SeasonsId)
                {
                    var toursPerSeason = toursRepo.GetList(
                        predicate: t => t.SeasonId == seasonId,
                        orderBy: q => q.OrderByDescending(t => t.Date),
                        size: size).Items;

                    tours.AddRange(toursPerSeason);
                }

                return tours.ToPaginate(0, tours.Count);
            }

            return toursRepo.GetList(
                orderBy: q => q.OrderByDescending(t => t.Date),
                size: size);
        }
    }
}