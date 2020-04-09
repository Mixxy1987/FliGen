using FliGen.Common.Messages;
using FliGen.Services.Leagues.Application.Dto;

namespace FliGen.Services.Leagues.Application.Commands.UpdateLeague
{
    [MessageNamespace("leagues")]
    public class UpdateLeague : ICommand
    {
        public int LeagueId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public LeagueType LeagueType { get; set; }
    }
}