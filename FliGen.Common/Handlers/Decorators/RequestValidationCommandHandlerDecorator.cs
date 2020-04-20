using FliGen.Common.Handlers.Extensions;
using FliGen.Common.Messages;
using FliGen.Common.RabbitMq;
using FliGen.Common.Types;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FliGen.Common.Handlers.Decorators
{
    public sealed class RequestValidationCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>,
        ICommandHandlerDecorator
        where TCommand : ICommand
    {
        private readonly IEnumerable<AbstractValidator<TCommand>> _validators;
        private readonly ICommandHandler<TCommand> _inner;

        public RequestValidationCommandHandlerDecorator(IEnumerable<AbstractValidator<TCommand>> validators, ICommandHandler<TCommand> inner)
        {
            _inner = inner ?? throw new ArgumentNullException(nameof(inner));
            _validators = validators ?? throw new ArgumentNullException(nameof(validators));
        }

        public async Task HandleAsync(TCommand command, ICorrelationContext context)
        {
            await Validate(command, context);
            await _inner.HandleAsync(command, context);
        }

        private async Task Validate(TCommand command, ICorrelationContext context)
        {
            if(_validators != null)
            {
                foreach (var validator in _validators)
                {
                    var result = await validator.ValidateAsync(command);
                    if (!result.IsValid)
                    {
                        throw new RequestValidationException(result.ToString());
                    }
                }
            }
        }

        public Type GetHandlerType()
        {
            return _inner.GetHandlerType();
        }
    }
}