using FliGen.Common.Extensions;
using FluentValidation;

namespace FliGen.Services.Players.Application.Commands.SendMessage
{
    public sealed class SendInboxNotificationValidator : AbstractValidator<SendInboxNotification>
    {
        public SendInboxNotificationValidator()
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

            RuleFor(c => c.Sender)
                .NotEmpty()
                .WithMessage("Не задано поле 'От кого'");
        }
    }
}
