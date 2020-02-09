using FliGen.Domain.Entities;
using FliGen.Domain.Repositories;
using FliGen.Application.Dto;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace FliGen.Application.Queries
{
    public class GetPlayersQueryHandler: IRequestHandler<GetPlayersQuery, IEnumerable<PlayerWithRate>>
    {
        private readonly IFLiGenRepository _repository;

        public GetPlayersQueryHandler(IFLiGenRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PlayerWithRate>> Handle(GetPlayersQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Player> players = await _repository.GetPlayersWithRatesAsync();

            return players.Select(x => new PlayerWithRate()
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Rate = x.Rates.OrderBy(y => y.Date).First().Rate
            });
        }
    }
}
