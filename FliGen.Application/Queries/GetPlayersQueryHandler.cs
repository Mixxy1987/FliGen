using FliGen.Domain.Entities;
using FliGen.Domain.Repositories;
using FliGen.Application.Dto;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FliGen.Application.Queries
{
    public class GetPlayersQueryHandler: IRequestHandler<GetPlayersQuery, GetPlayersResponse>
    {
        private readonly IFLiGenRepository _repository;

        public GetPlayersQueryHandler(IFLiGenRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetPlayersResponse> Handle(GetPlayersQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Player> players = await _repository.GetPlayersAsync();

            return new GetPlayersResponse()
            {
                Players = players
            };
        }
    }
}
