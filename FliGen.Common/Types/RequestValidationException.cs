using System;

namespace FliGen.Common.Types
{
    public sealed class RequestValidationException : Exception
    {
        public RequestValidationException(string message): base(message)
        {
        }   
    }
}