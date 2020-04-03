using FliGen.Common.Messages;
using Newtonsoft.Json;

namespace FliGen.Services.Api.Messages.Commands.Leagues
{
    [MessageNamespace("leagues")]
    public class JoinLeague : ICommand
    {
        public int LeagueId { get; set; }
        public string PlayerExternalId { get; set; }

        private JoinLeague() {}

        [JsonConstructor]
        public JoinLeague(int leagueId, string playerExternalId)
        {
            LeagueId = leagueId;
            PlayerExternalId = playerExternalId;
        }
    }
}