using System;
using FliGen.Common.Mediator.Decorators;

namespace FliGen.Common.Mediator.Extensions
{
    public static class HandlerDecoratorExtension
    {
        public static Type GetHandlerType(this object target)
        {
            return target is IHandlerDecorator d
                ? d.GetHandlerType()
                : target.GetType();
        }
    }
}