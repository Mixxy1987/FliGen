using FliGen.Common.Handlers.Extensions;
using FliGen.Common.Logging;
using FliGen.Common.Messages;
using FliGen.Common.RabbitMq;
using System;
using System.Threading.Tasks;

namespace FliGen.Common.Handlers.Decorators
{
    public sealed class RequestLogCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>,
        ICommandHandlerDecorator
        where TCommand : ICommand
    {
        private readonly ILogService _logService;
        private readonly ICommandHandler<TCommand> _inner;

        public RequestLogCommandHandlerDecorator(ILogService logService, ICommandHandler<TCommand> inner)
        {
            _logService = logService ?? throw new ArgumentNullException(nameof(logService));
            _inner = inner ?? throw new ArgumentNullException(nameof(inner));
        }

        public async Task HandleAsync(TCommand request, ICorrelationContext context)
        {
            Type requestType = request.GetType();
            var innerType = _inner.GetHandlerType();
            try
            {
                _logService.Trace(innerType, "Executing {name}:\r\n{@request}", requestType.Name, request);
                await _inner.HandleAsync(request, context);
                _logService.Trace(innerType, "Executed {name}:\r\n{@request}", requestType.Name);
            }
            catch (Exception)
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