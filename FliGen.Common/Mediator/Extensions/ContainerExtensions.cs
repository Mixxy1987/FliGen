using System.Collections.Generic;
using System.Reflection;
using Autofac;
using FliGen.Common.Mediator.Decorators;
using FliGen.Common.Mediator.Logging;
using FluentValidation;
using MediatR;

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
                return t => ctx.Resolve<IComponentContext>().Resolve(t);
            });
            return builder;
        }

        public static ContainerBuilder AddRequestValidationDecorator(this ContainerBuilder builder)
        {
            builder.RegisterGenericDecorator(typeof(RequestLogDecorator<,>), typeof(IRequestHandler<,>));
            return builder;
        }

        public static ContainerBuilder AddSerilogService(this ContainerBuilder builder)
        {
            builder.RegisterType<SerilogService>().As<ILogService>();
            return builder;
        }
    }
}