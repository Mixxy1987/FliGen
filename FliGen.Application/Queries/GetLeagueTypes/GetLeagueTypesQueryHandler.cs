using FliGen.Domain.Common.Repository;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FliGen.Application.Queries.GetLeagueTypes
{
    public class GetLeagueTypesQueryHandler : IRequestHandler<GetLeagueTypesQuery, IEnumerable<Dto.LeagueType>>
    {
        private readonly IUnitOfWork _uow;

        public GetLeagueTypesQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<Dto.LeagueType>> Handle(GetLeagueTypesQuery request, CancellationToken cancellationToken)
        {
            var repo = _uow.GetRepositoryAsync<Domain.Entities.Enum.LeagueType>();
            var leagueTypes = await repo.GetListAsync(cancellationToken: cancellationToken);

            var result = leagueTypes.Items.Select(x => new Dto.LeagueType(){ Name = x.Name }).ToArray(); //todo:: automapper?

            return result;
        }
    }
}
