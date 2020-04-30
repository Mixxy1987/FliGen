using FluentValidation;

namespace FliGen.Services.Tours.Application.Queries.Tours
{
    public sealed class ToursQueryValidator : AbstractValidator<ToursQuery>
    {
        public ToursQueryValidator()
        {
            RuleFor(c => c)
                .NotEmpty()
                .WithMessage("Не задан запрос на получение информации о турах");
        }
    }
}
