using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FliGen.Domain.Common.Repository;
using FliGen.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FliGen.Application.Queries.GetMyLeagues
{
    public class GetMyLeaguesQueryHandler : IRequestHandler<GetMyLeaguesQuery, IEnumerable<Dto.League>>
    {
        private readonly IUnitOfWork _uow;

        public GetMyLeaguesQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<Dto.League>> Handle(GetMyLeaguesQuery request, CancellationToken cancellationToken)
        {
            var playerRepo = _uow.GetRepositoryAsync<Player>();

            Player player = await playerRepo.SingleAsync(
                predicate: x => x.ExternalId == request.UserId,
                include: source => source.Include(a => a.LeaguePlayerLinks));

            var leagueRepo = _uow.GetRepositoryAsync<League>();

            var leagues = new List<League>();

            foreach (var link in player.LeaguePlayerLinks)
            {
                leagues.Add(await leagueRepo.SingleAsync(
                    predicate: x => x.Id == link.LeagueId));
            }

            var result = leagues.Select(x => new Dto.League() //todo::
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                LeagueType = new Dto.LeagueType()
                {
                    Name = x.Type.Name
                }
            }).ToArray(); //todo:: automapper?

            return result;
        }
    }
}