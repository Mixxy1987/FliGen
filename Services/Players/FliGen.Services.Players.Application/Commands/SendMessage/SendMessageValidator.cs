using FliGen.Common.Extensions;
using FluentValidation;

namespace FliGen.Services.Players.Application.Commands.SendMessage
{
    public sealed class SendMessageValidator : AbstractValidator<SendMessage>
    {
        public SendMessageValidator()
        {
            RuleFor(c => c)
                .NotEmpty()
                .WithMessage("Не задано сообщение");

            RuleFor(c => c.Topic)
                .NotEmpty()
                .WithMessage("Не задана тема");

            RuleFor(c => c.Body)
                .NotEmpty()
                .WithMessage("Не задано тело сообщения");

            RuleFor(c => c.From)
                .NotEmpty()
                .WithMessage("Не задано поле 'От кого'");
        }
    }
}
