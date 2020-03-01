using FliGen.Common.Extensions;
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

            RuleFor(c => c.Rate)
                .Must(x => double.TryParse(x.CommaToDot(), out double _))
                .WithMessage("Неверный формат рейтинга игрока");
        }
    }
}
