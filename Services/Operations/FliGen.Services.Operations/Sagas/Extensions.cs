﻿using Autofac;
using Chronicle;
using FliGen.Common.Messages;
using System;
using System.Linq;
using System.Reflection;

namespace FliGen.Services.Operations.Sagas
{
    internal static class Extensions
    {
        private static readonly Type[] SagaTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.IsAssignableTo<ISaga>())
            .ToArray();

        internal static bool BelongsToSaga<TMessage>(this TMessage _) where TMessage : IMessage
            => SagaTypes.Any(t => t.IsAssignableTo<ISagaAction<TMessage>>());
    }
}
