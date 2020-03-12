using System;
using FliGen.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using FliGen.Domain.Entities;
using FliGen.Domain.Entities.Enum;

namespace FliGen.Application.Commands.League.JoinLeague
{
    public class JoinLeagueCommandHandler : IRequestHandler<JoinLeagueCommand>
    {
        private readonly ILeagueRepository _leagueRepository;
        private readonly IPlayerRepository _playerRepository;

        public JoinLeagueCommandHandler(ILeagueRepository leagueRepository, IPlayerRepository playerRepository)
        {
	        _leagueRepository = leagueRepository;
	        _playerRepository = playerRepository;
        }

        public async Task<Unit> Handle(JoinLeagueCommand request, CancellationToken cancellationToken)
        {
	        var league = await _leagueRepository.GetByIdOrThrowAsync(request.LeagueId);

            var player = await _playerRepository.GetByExternalIdOrThrowAsync(request.PlayerExternalId);

            var link = new LeaguePlayerLink()
            {
	            LeagueId = league.Id,
	            PlayerId = player.Id,
	            JoinTime = DateTime.Now,
	            LeaguePlayerRoleId = LeaguePlayerRole.User.Id
            };

            await _leagueRepository.JoinLeagueAsync(link);
            
            return Unit.Value;
        }
    }
}
