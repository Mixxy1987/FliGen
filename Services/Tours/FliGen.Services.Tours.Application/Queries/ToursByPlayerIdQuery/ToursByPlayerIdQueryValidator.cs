using FluentValidation;

namespace FliGen.Services.Tours.Application.Queries.ToursByPlayerIdQuery
{
    public sealed class ToursByPlayerIdQueryValidator : AbstractValidator<ToursByPlayerIdQuery>
    {
        public ToursByPlayerIdQueryValidator()
        {
            RuleFor(c => c)
                .NotEmpty()
                .WithMessage("Не задана запрос на получение туров");

            RuleFor(c => c.QueryType)
                .IsInEnum()
                .WithMessage("Невалидное значение QueryType");
        }
    }
}
