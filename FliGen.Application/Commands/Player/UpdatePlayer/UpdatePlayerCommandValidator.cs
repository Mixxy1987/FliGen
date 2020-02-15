using FluentValidation;

namespace FliGen.Application.Commands.Player.UpdatePlayer
{
    public sealed class UpdatePlayerCommandValidator : AbstractValidator<UpdatePlayerCommand>
    {
        public UpdatePlayerCommandValidator()
        {
            RuleFor(c => c)
                .NotEmpty()
                .WithMessage("Не задана команда на обновление игрока");
        }
    }
}
