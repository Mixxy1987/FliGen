namespace FliGen.Common.SeedWork.Repository
{
    public interface IRepositoryReadOnly<T> : IReadRepository<T> where T : class
    {
       
    }
}