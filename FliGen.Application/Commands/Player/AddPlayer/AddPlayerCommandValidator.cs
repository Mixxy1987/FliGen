using FluentValidation;
using FliGen.Common.Mediator.Extensions;

namespace FliGen.Application.Commands.Player.AddPlayer
{
    public sealed class AddPlayerCommandValidator : AbstractValidator<AddPlayerCommand>
    {
        public AddPlayerCommandValidator()
        {
            RuleFor(c => c)
                .NotEmpty()
                .WithMessage("Не задана команда на добавление игрока");

            RuleFor(c => c.Rate)
                .Must(x => double.TryParse(x.DotToComma(), out double _))
                .WithMessage("Неверный формат рейтинга игрока");

            //todo:: add validation rules
        }
    }
}
