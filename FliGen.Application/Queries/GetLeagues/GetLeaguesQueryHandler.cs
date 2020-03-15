using FliGen.Domain.Common.Repository;
using FliGen.Domain.Common.Repository.Paging;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FliGen.Application.Queries.GetLeagues
{
    public class GetLeaguesQueryHandler : IRequestHandler<GetLeaguesQuery, IEnumerable<Dto.League>>
    {
        private readonly IUnitOfWork _uow;

        public GetLeaguesQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<Dto.League>> Handle(GetLeaguesQuery request, CancellationToken cancellationToken)
        {
            var repo = _uow.GetRepositoryAsync<Domain.Entities.League>();

            IPaginate<Domain.Entities.League> leagues = await repo.GetListAsync(cancellationToken : cancellationToken);

            var result = leagues.Items.Select(x => new Dto.League() //todo:: paging
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
