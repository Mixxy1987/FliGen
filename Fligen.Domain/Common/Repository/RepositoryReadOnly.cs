using Microsoft.EntityFrameworkCore;

namespace FliGen.Domain.Common.Repository
{
    public class RepositoryReadOnly<T> : BaseRepository<T>, IRepositoryReadOnly<T> where T : class
    {
        public RepositoryReadOnly(DbContext context) : base(context)
        {
        }
    }
}