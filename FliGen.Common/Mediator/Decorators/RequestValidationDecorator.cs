using FliGen.Common.Mediator.Extensions;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FliGen.Common.Types;

namespace FliGen.Common.Mediator.Decorators
{
    public sealed class RequestValidationDecorator<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>,
        IHandlerDecorator
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<AbstractValidator<TRequest>> _validators;
        private readonly IRequestHandler<TRequest, TResponse> _inner;

        public RequestValidationDecorator(IEnumerable<AbstractValidator<TRequest>> validators, IRequestHandler<TRequest, TResponse> inner)
        {
            _inner = inner ?? throw new ArgumentNullException(nameof(inner));
            _validators = validators ?? throw new ArgumentNullException(nameof(validators));
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            await Validate(request, cancellationToken);
            return await _inner.Handle(request, cancellationToken);
        }

        private async Task Validate(TRequest request, CancellationToken cancellationToken)
        {
            if(_validators != null)
            {
                foreach (var validator in _validators)
                {
                    var result = await validator.ValidateAsync(request, cancellationToken);
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