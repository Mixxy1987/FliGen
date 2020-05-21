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
using FliGen.Services.Tours.Application.Queries.LeaguesSeasonsIdQuery;

namespace FliGen.Services.Tours.Application.Queries.Tours
{
    public class ToursQueryHandler : IRequestHandler<ToursQuery, IEnumerable<TourDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ITeamsService _teamsService;
        private readonly ISeasonsService _seasonsService;
        private readonly ILeaguesService _leaguesService;

        public ToursQueryHandler(
            IUnitOfWork uow,
            IMapper mapper,
            ITeamsService teamsService,
            ISeasonsService seasonsService,
            ILeaguesService leaguesService)
        {
            _uow = uow;
            _mapper = mapper;
            _teamsService = teamsService;
            _seasonsService = seasonsService;
            _leaguesService = leaguesService;
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
                if (request.QueryType == ToursQueryType.Incoming)
                {
                    tours = await FilterByPlayerIdAndIncomingStatus(request, toursRepo);
                }
                else
                {
                    tours = await FilterByPlayerId(request, toursRepo, predicate);
                }
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

            await EnrichTourDtoByLeagueId(toursDtos);

            return toursDtos;
        }

        private async Task EnrichTourDtoByLeagueId(IReadOnlyCollection<TourDto> toursDtos)
        {
            var leaguesIdDtos = await _seasonsService.GetLeaguesSeasonsId(
                Array.Empty<int>(),
                toursDtos.Select(t => t.SeasonId).Distinct().ToArray(),
                LeaguesSeasonsIdQueryType.All);

            var dict = new Dictionary<int, int>();

            foreach (var dto in leaguesIdDtos)
            {
                dict[dto.SeasonId] = dto.LeagueId;
            }

            foreach (var dto in toursDtos)
            {
                dto.LeagueId = dict[dto.SeasonId];
            }
        }

        private async Task<IPaginate<Tour>> FilterByPlayerIdAndIncomingStatus(
            ToursQuery request,
            IRepositoryReadOnly<Tour> toursRepo)
        {
            var leaguesId = await _leaguesService.GetPlayerJoinedLeagues(request.PlayerId);
            var actualSeasons = await _seasonsService.GetLeaguesSeasonsId(
                leaguesId,
                Array.Empty<int>(),
                LeaguesSeasonsIdQueryType.Actual);

            var tours = new List<Tour>();

            foreach (var season in actualSeasons)
            {
                var tour = toursRepo.Single(
                    predicate: t => t.SeasonId == season.SeasonId,
                    orderBy: q => q.OrderByDescending(t => t.Date));

                if (tour != null && !tour.IsRegistrationClosed())
                {
                    tours.Add(tour);
                }
            }

            return tours.ToPaginate(0, tours.Count);
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

            predicate = predicate == null ?
                t => tourIds.Contains(t.Id) : 
                predicate.AndAlso(t => tourIds.Contains(t.Id));

            return toursRepo.GetList(
                predicate,
                size: tourIds.Count);
        }

        private static IPaginate<Tour> QueryForAllPlayers(
            ToursQuery request,
            IRepositoryReadOnly<Tour> toursRepo,
            Expression<Func<Tour, bool>> predicate)
        {
            if (request.QueryType == ToursQueryType.All)
            {
                return toursRepo.GetList(predicate);
            }
            //todo:: valdiate data
            int size = request.QueryType == ToursQueryType.Last ? (int)request.Last : 1;

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