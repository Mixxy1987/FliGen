using AutoMapper;
using FliGen.Common.SeedWork.Repository;
using FliGen.Common.Types;
using FliGen.Services.Leagues.Application.Dto;
using FliGen.Services.Leagues.Application.Dto.Enum;
using FliGen.Services.Leagues.Application.Services;
using FliGen.Services.Leagues.Domain.Common;
using FliGen.Services.Leagues.Domain.Entities;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FliGen.Services.Leagues.Application.Queries.LeagueInformation
{
    public class LeagueInformationQueryHandler : IRequestHandler<LeagueInformationQuery, LeagueInformationDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ISeasonsService _seasonService;

        public LeagueInformationQueryHandler(
            IUnitOfWork uow,
            IMapper mapper,
            ISeasonsService seasonService)
        {
            _uow = uow;
            _mapper = mapper;
            _seasonService = seasonService;
        }

        public async Task<LeagueInformationDto> Handle(LeagueInformationQuery request, CancellationToken cancellationToken)
        {
            var repo = _uow.GetReadOnlyRepository<League>();

            League league = repo.Single(l => l.Id == request.Id);
            if (league is null)
            {
                throw new FliGenException(
                    ErrorCodes.NoLeagueWithSuchId,
                    $"There is no league with id: {request.Id}");
            }
            var leaguesInfoDto = _mapper.Map<LeagueInformationDto>(league);

            var seasons = (await _seasonService.GetAsync(request.Id, null, SeasonsQueryType.All))?.ToList();
            if (seasons == null || seasons.Count == 0)
            {
                return leaguesInfoDto;
            }

            leaguesInfoDto.SeasonsCount = seasons.Count;

            foreach (var season in seasons)
            {
                leaguesInfoDto.ToursCount += season.ToursPlayed;
            }

            var lastSeason = seasons.OrderBy(s => s.Start).Last();

            if (DateTime.Parse(lastSeason.Start) <= DateTime.UtcNow)
            {
                leaguesInfoDto.CurrentSeason = lastSeason;
            }

            return leaguesInfoDto;
        }
    }
}
