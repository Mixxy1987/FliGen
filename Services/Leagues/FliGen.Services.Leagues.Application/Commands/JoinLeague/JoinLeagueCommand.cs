using MediatR;

namespace FliGen.Services.Leagues.Application.Commands.JoinLeague
{
    public class JoinLeagueCommand : IRequest
    {
        public int LeagueId { get; set; }
        public string PlayerExternalId { get; set; }
    }
}