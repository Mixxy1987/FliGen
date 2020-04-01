using FliGen.Common.Extensions;
using FluentValidation;

namespace FliGen.Services.Players.Application.Commands.UpdatePlayer
{
    public sealed class UpdatePlayerValidator : AbstractValidator<UpdatePlayer>
    {
        public UpdatePlayerValidator()
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
