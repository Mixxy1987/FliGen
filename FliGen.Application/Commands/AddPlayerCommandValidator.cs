using FluentValidation;

namespace FliGen.Application.Commands
{
    public sealed class AddPlayerCommandValidator : AbstractValidator<AddPlayerCommand>
    {
        public AddPlayerCommandValidator()
        {
            RuleFor(c => c)
                .NotEmpty()
                .WithMessage("Не задана команда на добавление игрока");

            //todo:: add validation rules
        }
    }
}
