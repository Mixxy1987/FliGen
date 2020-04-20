using FliGen.Common.Extensions;
using FluentValidation;

namespace FliGen.Services.Players.Application.Commands.AddPlayer
{
    public sealed class AddPlayerValidator : AbstractValidator<AddPlayer>
    {
        public AddPlayerValidator()
        {
            RuleFor(c => c)
                .NotEmpty()
                .WithMessage("Не задана команда на добавление игрока");

            RuleFor(c => c.FirstName)
                .NotEmpty()
                .WithMessage("Неверное имя");

            RuleFor(c => c.LastName)
                .NotEmpty()
                .WithMessage("Неверная фамилия");

            RuleFor(c => c.Rate)
                .Must(x => x == null || double.TryParse(x.CommaToDot(), out _))
                .WithMessage("Неверный формат рейтинга игрока");
        }
    }
}
