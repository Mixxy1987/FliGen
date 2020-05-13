using FliGen.Common.Messages;

namespace FliGen.Services.Operations.Messages.Leagues.Commands
{
    [MessageNamespace("leagues")]
    public class UpdateLeague : ICommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public LeagueType LeagueType { get; set; }
    }
}