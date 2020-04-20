using System;

namespace FliGen.Common.Handlers.Decorators
{
    public interface ICommandHandlerDecorator
    {
        Type GetHandlerType();
    }
}
