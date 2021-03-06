﻿using System;
using AutoMapper;
using FliGen.Common.SeedWork.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FliGen.Services.Leagues.Application.Queries.LeagueSettings
{
    public class LeagueSettingsQueryHandler : IRequestHandler<LeagueSettingsQuery, Dto.LeagueSettings>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public LeagueSettingsQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
	        _uow = uow;
	        _mapper = mapper;
        }

        public async Task<Dto.LeagueSettings> Handle(LeagueSettingsQuery request, CancellationToken cancellationToken)
        {
            var leagueSettingsRepo = _uow.GetRepositoryAsync<Domain.Entities.LeagueSettings>();
            var settings = await leagueSettingsRepo.SingleAsync(x => x.LeagueId == request.LeagueId);

            return _mapper.Map<Dto.LeagueSettings>(settings);
        }
    }
}
