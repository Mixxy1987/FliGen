using FliGen.Domain.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FliGen.Application.Queries.GetMyLeagues
{
    public class GetLeagueTypesQueryHandler : IRequestHandler<GetMyLeaguesQuery, IEnumerable<Dto.League>>
    {
        private readonly ILeagueRepository _repository;

        public GetLeagueTypesQueryHandler(ILeagueRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Dto.League>> Handle(GetMyLeaguesQuery request, CancellationToken cancellationToken)
        {
            var leagues = await _repository.GetLeaguesAsync();

            var result = leagues.Select(x => new Dto.League()
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
