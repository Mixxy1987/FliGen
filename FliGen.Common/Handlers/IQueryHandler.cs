using System.Threading.Tasks;
using FliGen.Common.Types;

namespace FliGen.Common.Handlers
{
    public interface IQueryHandler<TQuery,TResult> where TQuery : IQuery<TResult>
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}