using MediatR;

namespace FliGen.Services.Leagues.Application.Commands.UpdateLeagueSettings
{
    public class UpdateLeagueSettingsCommand : IRequest
    {
        public int LeagueId { get; set; }
        public bool Visibility { get; set; }
        public bool RequireConfirmation { get; set; }
    }
}