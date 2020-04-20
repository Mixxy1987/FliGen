using Autofac;
using FliGen.Common.Handlers.Decorators;

namespace FliGen.Common.Handlers.Extensions
{
    public static class ContainerExtensions
    {
        public static ContainerBuilder AddRequestValidationCommandHandlerDecorator(this ContainerBuilder builder)
        {
            builder.RegisterGenericDecorator(typeof(RequestValidationCommandHandlerDecorator<>), typeof(ICommandHandler<>));
            return builder;
        }

        public static ContainerBuilder AddRequestLogCommandHandlerDecorator(this ContainerBuilder builder)
        {
            builder.RegisterGenericDecorator(typeof(RequestLogCommandHandlerDecorator<>), typeof(ICommandHandler<>));
            return builder;
        }
    }
}