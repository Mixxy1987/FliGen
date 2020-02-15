using FliGen.Domain.Entities;
using FliGen.Domain.Repositories;
using FliGen.Application.Dto;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using FluentValidation;

namespace FliGen.Application.Queries
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
                FirstName = x.FirstName,
                LastName = x.LastName,
                Rate = 7.0//x.Rates.OrderBy(y => y.Date).First().Rate //todo::temp
            });
        }
    }
}
