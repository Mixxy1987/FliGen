using FliGen.Common.Messages;
using FliGen.Services.Leagues.Application.Dto;

namespace FliGen.Services.Leagues.Application.Commands.CreateLeague
{
    [MessageNamespace("leagues")]
    public class CreateLeague : ICommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public LeagueType LeagueType { get; set; }
    }
}