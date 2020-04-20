using Autofac;
using FliGen.Common.Handlers;
using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Reflection;
using FliGen.Common.Handlers.Decorators;
using FliGen.Common.Logging;

namespace FliGen.Common.Mediator.Extensions
{
    public static class ContainerExtensions
    {
        public static ContainerBuilder AddMediator(this ContainerBuilder builder, params string[] assemblies)
        {
            var result = new List<Assembly>();
            foreach (var assembly in assemblies)
            {
                result.Add(Assembly.Load(assembly));
            }

            return AddMediator(builder, result.ToArray());
        }

        public static ContainerBuilder AddMediator(this ContainerBuilder builder, params Assembly[] assemblies)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(assemblies).AsClosedTypesOf(typeof(IRequestHandler<,>));
            builder.RegisterAssemblyTypes(assemblies).AsClosedTypesOf(typeof(INotificationHandler<>));
            builder.RegisterAssemblyTypes(assemblies).AsClosedTypesOf(typeof(AbstractValidator<>));
            builder.Register<ServiceFactory>(ctx =>
            {
                var context = ctx.Resolve<IComponentContext>();
                return t => context.Resolve(t);
            });
            return builder;
        }

        public static ContainerBuilder AddRequestValidationDecorator(this ContainerBuilder builder)
        {
            builder.RegisterGenericDecorator(typeof(RequestValidationCommandHandlerDecorator<>), typeof(ICommandHandler<>));
            return builder;
        }

        public static ContainerBuilder AddRequestLogDecorator(this ContainerBuilder builder)
        {
            builder.RegisterGenericDecorator(typeof(Decorators.RequestLogDecorator<,>), typeof(IRequestHandler<,>));
            return builder;
        }

        public static ContainerBuilder AddSerilogService(this ContainerBuilder builder)
        {
            builder.RegisterType<SerilogService>().As<ILogService>();
            return builder;
        }
    }
}