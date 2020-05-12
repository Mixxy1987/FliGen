using System.Threading.Tasks;
using Chronicle;
using FliGen.Common.Handlers;
using FliGen.Common.Messages;
using FliGen.Common.RabbitMq;
using FliGen.Services.Operations.Sagas;
using FliGen.Services.Operations.Services;
using SagaContext = FliGen.Services.Operations.Sagas.SagaContext;

namespace FliGen.Services.Operations.Handlers
{
    public class GenericCommandHandler<T> : ICommandHandler<T> where T : class, ICommand
    {
        private readonly ISagaCoordinator _sagaCoordinator;
        private readonly IOperationPublisher _operationPublisher;
        private readonly IOperationsStorage _operationsStorage;

        public GenericCommandHandler(ISagaCoordinator sagaCoordinator,
            IOperationPublisher operationPublisher,
            IOperationsStorage operationsStorage)
        {
            _sagaCoordinator = sagaCoordinator;
            _operationPublisher = operationPublisher;
            _operationsStorage = operationsStorage;
        }

        public async Task HandleAsync(T command, ICorrelationContext context)
        {
            if (command.BelongsToSaga())
            {
                var sagaContext = SagaContext.FromCorrelationContext(context);
                await _sagaCoordinator.ProcessAsync(command, sagaContext);
            }

            switch (command)
            {
                case IRejectedEvent rejectedEvent:
                    await _operationsStorage.SetAsync(context.Id, context.UserId,
                        context.Name, OperationState.Rejected, context.Resource,
                        rejectedEvent.Code, rejectedEvent.Reason);
                    await _operationPublisher.RejectAsync(context,
                        rejectedEvent.Code, rejectedEvent.Reason);
                    return;
                case ICommand _:
                    await _operationsStorage.SetAsync(context.Id, context.UserId,
                        context.Name, OperationState.Completed, context.Resource);
                    await _operationPublisher.CompleteAsync(context);
                    return;
            }
        }
    }
}