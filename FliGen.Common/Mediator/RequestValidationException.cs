using System;

namespace FliGen.Common.Mediator
{
    public sealed class RequestValidationException : Exception
    {
        public RequestValidationException(string message): base(message)
        {
        }   
    }
}