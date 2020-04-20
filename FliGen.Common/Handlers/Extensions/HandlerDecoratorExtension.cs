using FliGen.Common.Handlers.Decorators;
using System;

namespace FliGen.Common.Handlers.Extensions
{
    public static class HandlerDecoratorExtension
    {
        public static Type GetHandlerType(this object target)
        {
            return target is ICommandHandlerDecorator d
                ? d.GetHandlerType()
                : target.GetType();
        }
    }
}