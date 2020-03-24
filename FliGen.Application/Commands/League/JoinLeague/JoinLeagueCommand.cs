using MediatR;

namespace FliGen.Application.Commands.League.JoinLeague
{
    public class JoinLeagueCommand : IRequest
    {
        public int LeagueId { get; set; }
        public string PlayerExternalId { get; set; }
    }
}