using System;
using System.Threading;
using System.Threading.Tasks;
using FliGen.Common.Mediator.Extensions;
using FliGen.Common.Mediator.Logging;
using MediatR;

namespace FliGen.Common.Mediator.Decorators
{
    public sealed class RequestLogDecorator<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>,
        IHandlerDecorator
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogService _logService;
        private readonly IRequestHandler<TRequest, TResponse> _inner;

        public RequestLogDecorator(ILogService logService, IRequestHandler<TRequest, TResponse> inner)
        {
            _logService = logService ?? throw new ArgumentNullException(nameof(logService));
            _inner = inner ?? throw new ArgumentNullException(nameof(inner));
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            Type requestType = request.GetType();
            var innerType = _inner.GetHandlerType();
            try
            {
                _logService.Trace(innerType, "Executing {name}:\r\n{@request}", requestType.Name, request);
                var response = await _inner.Handle(request, cancellationToken);
                if (response == null)
                    _logService.Trace(innerType, "Request {request} returned null", requestType.Name);
                return response;
            }
            catch (Exception e)
            {
                _logService.Error(innerType, "Error at {name}", requestType.Name);
                throw;
            }
        }

        public Type GetHandlerType()
        {
            return _inner.GetHandlerType();
        }
    }
}