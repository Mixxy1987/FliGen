using System;
using System.Collections.Generic;

namespace FliGen.Domain.Common
{
    public interface IReadOnlyRepository<T> where T : Entity, IAggregateRoot
    {
        T FindById(Guid id);

        IEnumerable<T> GetAll();
    }
}