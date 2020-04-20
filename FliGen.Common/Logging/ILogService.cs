using System;

namespace FliGen.Common.Logging
{
    public interface ILogService
    {
        void Trace(object obj, string message, params object[] parameters);
        void Error(object obj, string message, params object[] parameters);
        void Error(object obj, Exception exception, string message, params object[] parameters); 
        void Info(object obj, string message, params object[] parameters);
    }
}