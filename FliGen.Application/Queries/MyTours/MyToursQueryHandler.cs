using AutoMapper;
using FliGen.Application.Dto;
using FliGen.Domain.Common.Repository;
using FliGen.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Player = FliGen.Domain.Entities.Player;

namespace FliGen.Application.Queries.MyTours
{
	public class MyToursQueryHandler : IRequestHandler<MyToursQuery, IEnumerable<Dto.Tour>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public MyToursQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Dto.Tour>> Handle(MyToursQuery request, CancellationToken cancellationToken)
        {
	        var playerRepo = _uow.GetRepositoryAsync<Player>();
	        var leagueRepo = _uow.GetRepositoryAsync<Domain.Entities.League>();

			Player player = await playerRepo.SingleAsync(
		        predicate: x => x.ExternalId == request.UserId,
		        include: source => source.Include(a => a.LeaguePlayerLinks));

	        List<LeaguePlayerLink> distinctLinks = player.LeaguePlayerLinks
		        .OrderBy(p => p.CreationTime)
		        .Where(l => l.InJoinedStatus())
		        .GroupBy(p => p.LeagueId)
		        .Select(g => g.Last())
		        .ToList();

	        var leagues = new List<Domain.Entities.League>();

	        foreach (var link in distinctLinks)
	        {
				leagues.Add(
					await leagueRepo
						.SingleAsync(
							predicate: l => l.Id == link.LeagueId,
							include: q => q
								.Include(l => l.Seasons)
								.ThenInclude(s => s.Tours)));
			}

	        var tours = new List<Dto.Tour>();

	        foreach (var league in leagues)
	        {
		        var tour = league.Seasons
			        .OrderBy(s => s.Start)
			        .Last().Tours
			        .Where(t => t.Date >= DateTime.Now)
			        .OrderBy(t => t.Date)
			        .FirstOrDefault();

		        tours.Add(_mapper.Map<Dto.Tour>(tour));
	        }

            return tours;
        }
    }
}