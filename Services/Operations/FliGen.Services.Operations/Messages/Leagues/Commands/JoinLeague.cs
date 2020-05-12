using FliGen.Common.Messages;

namespace FliGen.Services.Operations.Messages.Leagues.Commands
{
    [MessageNamespace("leagues")]
    public class JoinLeague : ICommand
    {
        public int LeagueId { get; set; }
        public string PlayerExternalId { get; set; }
    }
}