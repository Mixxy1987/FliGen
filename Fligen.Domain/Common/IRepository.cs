using System;

namespace FliGen.Domain.Common
{
    public interface IRepository<T> : IReadOnlyRepository<T> where T : Entity, IAggregateRoot
    {
        void Add(T item);
        void Remove(T item);
        void Update(T item);
        void Delete(Guid id);
    }
}