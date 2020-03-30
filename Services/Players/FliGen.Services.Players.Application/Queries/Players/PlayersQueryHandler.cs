using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FliGen.Common.SeedWork.Repository;
using FliGen.Common.SeedWork.Repository.Paging;
using FliGen.Services.Players.Application.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FliGen.Services.Players.Application.Queries.Players
{
    public class PlayersQueryHandler: IRequestHandler<PlayersQuery, IEnumerable<PlayerWithRate>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public PlayersQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PlayerWithRate>> Handle(PlayersQuery request, CancellationToken cancellationToken)
        {
            var repo = _uow.GetRepositoryAsync<Domain.Entities.Player>();

            IPaginate<Domain.Entities.Player> players = await repo.GetListAsync(
                include: source => source.Include(a => a.Rates),
                size: request.Size,
                cancellationToken: cancellationToken);

            if (players.Count == 0)
            {
                return null;
            }

            IEnumerable<PlayerWithRate> collection = players.Items.Select(x => _mapper.Map<Dto.PlayerWithRate>(x));

            return collection;
        }
    }
}
