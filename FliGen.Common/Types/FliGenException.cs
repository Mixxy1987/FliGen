using System;

namespace FliGen.Common.Types
{
	public class FliGenException : Exception
	{
		public string Code { get; }

		public FliGenException()
		{
		}

		public FliGenException(string code)
		{
			Code = code;
		}

		public FliGenException(string message, params object[] args)
			: this(string.Empty, message, args)
		{
		}

		public FliGenException(string code, string message, params object[] args)
			: this(null, code, message, args)
		{
		}

		public FliGenException(Exception innerException, string message, params object[] args)
			: this(innerException, string.Empty, message, args)
		{
		}

		public FliGenException(Exception innerException, string code, string message, params object[] args)
			: base(string.Format(message, args), innerException)
		{
			Code = code;
		}
    }
}