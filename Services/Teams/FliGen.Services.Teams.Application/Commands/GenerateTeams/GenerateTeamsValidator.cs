using FluentValidation;

namespace FliGen.Services.Teams.Application.Commands.GenerateTeams
{
    public sealed class GenerateTeamsValidator : AbstractValidator<GenerateTeams>
    {
        public GenerateTeamsValidator()
        {
            RuleFor(c => c)
                .NotEmpty()
                .WithMessage("Не задана команда на создание команд");
        }
    }
}
