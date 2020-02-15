using System;
using System.Text;

namespace FliGen.Common.Mediator.Logging
{
    public sealed class SerilogService : ILogService
    {
        public void Trace(object obj, string message, params object[] parameters)
        {
            Serilog.Log.ForContext("SourceContext", GetContext(obj)).Verbose(message, parameters);
        }

        public void Error(object obj, string message, params object[] parameters)
        {
            Serilog.Log.ForContext("SourceContext", GetContext(obj)).Error(message, parameters);
        }

        public void Error(object obj, Exception exception, string message, params object[] parameters)
        {
            Serilog.Log.ForContext("SourceContext", GetContext(obj)).Error(exception, message, parameters);
        }

        public void Info(object obj, string message, params object[] parameters)
        {
            Serilog.Log.ForContext("SourceContext", GetContext(obj)).Information(message, parameters);
        }

        private string GetContext(object obj)
        {
            if (obj is Type type)
            {
                return GetContext(type, true);
            }

            return GetContext(obj.GetType(), true);
        }

        private string GetContext(Type type, bool root)
        {
            var builder = new StringBuilder();
            if (root)
            {
                builder.Append(type.Namespace).Append('.');
            }

            if (type.IsGenericType)
            {
                builder.Append(CleanTypeName(type.Name)).Append('[');
                var arguments = type.GetGenericArguments();
                for (int i = 0; i < arguments.Length; i++)
                {
                    var argument = arguments[i];
                    builder.Append(GetContext(argument, false));

                    if (i < arguments.Length - 1)
                    {
                        builder.Append(", ");
                    }
                }
            }
            else
            {
                builder.Append(type.Name);
            }

            return builder.ToString();
        }

        private static string CleanTypeName(string name)
        {
            return name.Remove(name.IndexOf(',')); //todo:: what here?
        }
    }
}
