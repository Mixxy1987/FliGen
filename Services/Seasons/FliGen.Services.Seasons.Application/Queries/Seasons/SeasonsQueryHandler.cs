using AutoMapper;
using FliGen.Common.SeedWork.Repository;
using FliGen.Services.Seasons.Application.Dto;
using FliGen.Services.Seasons.Application.Dto.Enum;
using FliGen.Services.Seasons.Application.Queries.Tours;
using FliGen.Services.Seasons.Application.Services;
using FliGen.Services.Seasons.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FliGen.Common.Types;
using FliGen.Services.Seasons.Domain.Common;

namespace FliGen.Services.Seasons.Application.Queries.Seasons
{
    public class SeasonsQueryHandler : IRequestHandler<SeasonsQuery, IEnumerable<SeasonDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IToursService _toursService;

        public SeasonsQueryHandler(
            IUnitOfWork uow,
            IToursService toursService)
        {
	        _uow = uow;
            _toursService = toursService;
        }

        public async Task<IEnumerable<SeasonDto>> Handle(SeasonsQuery request, CancellationToken cancellationToken)
        {
            var seasonsRepo = _uow.GetReadOnlyRepository<Season>();
            int[] seasonsId = request.SeasonsId;

            if (request.SeasonsId is null ||
                request.SeasonsId.Length == 0)
            {
                if (request.LeagueId == 0)
                {
                    throw new FliGenException(ErrorCodes.InvalidRequest, "seasonsId or leagueId should be set");
                }

                var seasons = seasonsRepo.GetList(
                    s => request.LeagueId == s.LeagueId,
                    size: Consts.SeasonsInLeagueMax).Items;

                seasonsId = request.QueryType == SeasonsQueryType.Last
                    ? new[] {seasons.OrderBy(s => s.Start).Last().Id}
                    : seasonsId = seasons.Select(s => s.Id).ToArray();
            }

            var dtos = (await _toursService.GetAsync(0, seasonsId, ToursQueryType.Last, 2)).ToList();

            var seasonDtos = new List<SeasonDto>();

            foreach (var seasonId in seasonsId)
            {
                var sortedTourDtos = dtos
                    .Where(t => t.SeasonId == seasonId)
                    .OrderBy(t => t.Date)
                    .ToList();

                Season season = seasonsRepo.Single(s => s.Id == seasonId);

                var seasonDto = new SeasonDto
                {
                    SeasonId = seasonId,
                    Start = season.Start.ToString("yyyy-MM-dd"),
                    Finish =  season.Finish.ToString("yyyy-MM-dd"),
                    ToursPlayed = (await _toursService.GetSeasonStatsAsync(seasonId)).Count
                };
                switch (sortedTourDtos.Count)
                {
                    case 2:
                    {
                        if (sortedTourDtos[1].TourStatus == TourStatus.Completed ||
                            sortedTourDtos[1].TourStatus == TourStatus.Canceled)
                        {
                            seasonDto.PreviousTour = sortedTourDtos[1];
                            seasonDto.NextTour = null;
                        }
                        else
                        {
                            seasonDto.PreviousTour = sortedTourDtos[0];
                            seasonDto.NextTour = sortedTourDtos[1];
                        }
                        break;
                    }
                    case 1:
                    {
                        if (sortedTourDtos[0].TourStatus == TourStatus.Completed ||
                            sortedTourDtos[1].TourStatus == TourStatus.Canceled)
                        {
                            seasonDto.PreviousTour = sortedTourDtos[0];
                        }
                        else
                        {
                            seasonDto.NextTour = sortedTourDtos[0];
                        }
                        break;
                    }
                    case 0:
                    {
                        seasonDto.PreviousTour = null;
                        seasonDto.NextTour = null;
                        break;
                    }
                }

                seasonDtos.Add(seasonDto);
            }


            return seasonDtos;
        }
    }
}
