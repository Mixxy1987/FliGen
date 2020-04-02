using FliGen.Common.Messages;
using FliGen.Services.Api.Models.Leagues;
using Newtonsoft.Json;

namespace FliGen.Services.Api.Messages.Commands.Leagues
{
    [MessageNamespace("leagues")]
    public class CreateLeague : ICommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public LeagueType LeagueType { get; set; }

        private CreateLeague() {}

        [JsonConstructor]
        public CreateLeague(string name, string description, LeagueType leagueType)
        {
            Name = name;
            Description = description;
            LeagueType = leagueType;
        }
    }
}