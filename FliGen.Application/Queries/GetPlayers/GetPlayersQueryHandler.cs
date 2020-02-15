using FliGen.Application.Dto;
using FliGen.Domain.Entities;
using FliGen.Domain.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FliGen.Application.Queries.GetPlayers
{
    public class GetPlayersQueryHandler: IRequestHandler<GetPlayersQuery, IEnumerable<PlayerWithRate>>
    {
        private readonly IPlayerRepository _repository;

        public GetPlayersQueryHandler(IPlayerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PlayerWithRate>> Handle(GetPlayersQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Player> players = await _repository.GetAsync();

            return players.Select(x => new PlayerWithRate()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Rate = x.Rates.OrderBy(y => y.Date).Last().Value
            });
        }
    }
}
