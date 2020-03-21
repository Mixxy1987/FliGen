using FliGen.Application.Dto;
using MediatR;

namespace FliGen.Application.Commands.League.UpdateLeagueSettings
{
    public class UpdateLeagueSettingsCommand : IRequest
    {
        public int LeagueId { get; set; }
        public bool Visibility { get; set; }
        public bool RequireConfirmation { get; set; }
    }
}