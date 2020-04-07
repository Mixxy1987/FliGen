using System;

namespace FliGen.Common.Types
{
    public interface IIdentifiable
    {
        Guid Id { get; }
    }
}