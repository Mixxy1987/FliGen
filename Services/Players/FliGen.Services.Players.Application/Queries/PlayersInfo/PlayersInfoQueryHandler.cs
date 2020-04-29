using FliGen.Common.SeedWork.Repository;
using FliGen.Common.SeedWork.Repository.Paging;
using FliGen.Services.Players.Application.Dto;
using FliGen.Services.Players.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FliGen.Services.Players.Application.Queries.PlayersInfo
{
    public class PlayersInfoQueryHandler : IRequestHandler<PlayersInfoQuery, PlayersInfoDto>
    {
        private readonly IUnitOfWork _uow;

        public PlayersInfoQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<PlayersInfoDto> Handle(PlayersInfoQuery request, CancellationToken cancellationToken)
        {
            var repo = _uow.GetReadOnlyRepository<Player>();

            IPaginate<Player> players = repo.GetList();

            var playersInfoDto = new PlayersInfoDto()
            {
                Count = players.Count
            };
           
            return playersInfoDto;
        }
    }
}
