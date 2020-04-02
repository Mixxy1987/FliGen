using AutoMapper;
using FliGen.Common.SeedWork.Repository;
using FliGen.Services.Players.Application.Dto;
using FliGen.Services.Players.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FliGen.Services.Players.Application.Queries.PlayerInternalId
{
    public class PlayerInternalIdQueryHandler : IRequestHandler<PlayerInternalIdQuery, PlayerInternalIdDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public PlayerInternalIdQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<PlayerInternalIdDto> Handle(PlayerInternalIdQuery request, CancellationToken cancellationToken)
        {
            var repo = _uow.GetRepositoryAsync<Player>();

            Player player = await repo.SingleAsync(p => p.ExternalId == request.ExternalId);

            if (player is null)
            {
                return null;
            }

            return _mapper.Map<PlayerInternalIdDto>(_mapper.Map<PlayerInternalIdDto>(player));
        }
    }
}
