using FliGen.Application.Dto;
using FliGen.Common.SeedWork.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FliGen.Common.SeedWork.Repository.Paging;

namespace FliGen.Application.Queries.GetPlayers
{
    public class GetPlayersQueryHandler: IRequestHandler<GetPlayersQuery, IEnumerable<PlayerWithRate>>
    {
        private readonly IUnitOfWork _uow;

        public GetPlayersQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<PlayerWithRate>> Handle(GetPlayersQuery request, CancellationToken cancellationToken)
        {
            var repo = _uow.GetRepositoryAsync<Domain.Entities.Player>();

            IPaginate<Domain.Entities.Player> players = await repo.GetListAsync(
                include: source => source.Include(a => a.Rates),
                size: 30,
                cancellationToken: cancellationToken);

            return players.Items.Select(x => new PlayerWithRate()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Rate = x.Rates.OrderBy(y => y.Date).Last().Value
            });
        }
    }
}
