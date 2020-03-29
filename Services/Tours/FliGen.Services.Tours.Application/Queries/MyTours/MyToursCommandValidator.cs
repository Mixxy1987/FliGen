using FluentValidation;

namespace FliGen.Services.Tours.Application.Queries.MyTours
{
    public sealed class MyToursCommandValidator : AbstractValidator<MyToursQuery>
    {
        public MyToursCommandValidator()
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
