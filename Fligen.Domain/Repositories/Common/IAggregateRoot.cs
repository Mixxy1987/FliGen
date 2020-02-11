using System;

namespace FliGen.Domain.Repositories.Common
{
	/// <summary>
	/// The aggregate root for use in the repository.
	/// </summary>
	/// <remarks>
	/// This indicates what objects can be directly loaded from the repository.
	/// </remarks>
	public interface IAggregateRoot
	{
		/// <summary>
		/// Gets or sets the id.
		/// </summary>
		Guid Id { get; set; }
	}
}