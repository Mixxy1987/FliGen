namespace FliGen.Common
{
	/// <summary>
	/// Результат выполнения void операции. 
	/// </summary>
	public class Result
	{
		public bool Success { get; private set; }
		public string Message { get; private set; }

		protected Result(bool success, string message) => (Success, Message) = (success, message);

		public static Result Ok(string message = "") => new Result(true, message);

		public static Result Fail(string message) => new Result(false, message);

		public static Result<T> Ok<T>(T value, string message = "") => new Result<T>(value, true, message);

		public static Result<T> Fail<T>(string message) => new Result<T>(default(T), false, message);
	}

	/// <summary>
	/// Результат выполнения void операции. 
	/// </summary>
	public class Result<T> : Result
	{
		public T Value { get; private set; }

		protected internal Result(T value, bool success, string message)
			: base(success, message) => Value = value;
	}
}