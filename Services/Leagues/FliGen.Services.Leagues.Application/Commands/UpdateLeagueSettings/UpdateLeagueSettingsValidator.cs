using FluentValidation;

namespace FliGen.Services.Leagues.Application.Commands.UpdateLeagueSettings
{
    public sealed class UpdateLeagueSettingsValidator : AbstractValidator<UpdateLeagueSettings>
    {
        private const int PlayersInTeamMinCount = 2;
        private const int TeamsInTourMinCount = 2;

        public UpdateLeagueSettingsValidator()
        {
            RuleFor(c => c)
                .NotEmpty()
                .WithMessage("Please set command to update league settings");

            RuleFor(c => c.PlayersInTeam)
                .GreaterThanOrEqualTo(PlayersInTeamMinCount)
                .WithMessage("Invalid players in team count");

            RuleFor(c => c.TeamsInTour)
                .GreaterThanOrEqualTo(TeamsInTourMinCount)
                .WithMessage("Invalid teams in tour count");
        }
    }
}
