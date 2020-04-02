using FliGen.Services.Leagues.Application.Dto;
using MediatR;

namespace FliGen.Services.Leagues.Application.Commands.UpdateLeague
{
    public class UpdateLeagueCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public LeagueType LeagueType { get; set; }
    }
}