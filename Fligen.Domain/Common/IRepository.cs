using System;

namespace FliGen.Domain.Common
{
    public interface IRepository<T> : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}