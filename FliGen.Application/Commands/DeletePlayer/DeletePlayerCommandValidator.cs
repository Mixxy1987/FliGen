using FluentValidation;

namespace FliGen.Application.Commands.DeletePlayer
{
    public sealed class DeletePlayerCommandValidator : AbstractValidator<DeletePlayerCommand>
    {
        public DeletePlayerCommandValidator()
        {
            RuleFor(c => c)
                .NotEmpty()
                .WithMessage("Не задана команда на удаление игрока");
        }
    }
}
