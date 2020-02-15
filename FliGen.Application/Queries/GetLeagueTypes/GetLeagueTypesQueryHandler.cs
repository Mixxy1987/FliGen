using FliGen.Application.Dto;
using FliGen.Domain.Entities;
using FliGen.Domain.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FliGen.Application.Queries.GetLeagueTypes
{
    public class GetLeagueTypesQueryHandler : IRequestHandler<GetLeagueTypesQuery, IEnumerable<Dto.LeagueType>>
    {
        private readonly ILeagueRepository _repository;

        public GetLeagueTypesQueryHandler(ILeagueRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Dto.LeagueType>> Handle(GetLeagueTypesQuery request, CancellationToken cancellationToken)
        {
            var leagueTypes = await _repository.GetTypes();

            var result = leagueTypes.Select(x => new Dto.LeagueType(){Value = x.Name}).ToArray(); //todo:: automapper?

            return result;
        }
    }
}
