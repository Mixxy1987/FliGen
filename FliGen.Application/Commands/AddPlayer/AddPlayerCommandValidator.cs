using FluentValidation;

namespace FliGen.Application.Commands.AddPlayer
{
    public sealed class AddPlayerCommandValidator : AbstractValidator<AddPlayerCommand>
    {
        public AddPlayerCommandValidator()
        {
            RuleFor(c => c)
                .NotEmpty()
                .WithMessage("Не задана команда на добавление игрока");

            RuleFor(c => c.Rate)
                .Must(x => double.TryParse(x, out double _))
                .WithMessage("Неверный формат рейтинга игрока");

            //todo:: add validation rules
        }
    }
}
