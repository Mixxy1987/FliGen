using System.Threading.Tasks;
using FliGen.Common.Messages;
using FliGen.Common.RabbitMq;

namespace FliGen.Common.Handlers
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task HandleAsync(TCommand command, ICorrelationContext context);
    }
}