using FliGen.Common.Messages;

namespace FliGen.Services.Operations.Messages.Leagues.Commands
{
    [MessageNamespace("leagues")]
    public class CreateLeague : ICommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public LeagueType LeagueType { get; set; }
    }
}