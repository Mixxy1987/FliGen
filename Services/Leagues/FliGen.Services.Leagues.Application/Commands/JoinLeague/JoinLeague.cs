using FliGen.Common.Messages;

namespace FliGen.Services.Leagues.Application.Commands.JoinLeague
{
    [MessageNamespace("leagues")]
    public class JoinLeague : ICommand
    {
        public int LeagueId { get; set; }
        public string PlayerExternalId { get; set; }
    }
}