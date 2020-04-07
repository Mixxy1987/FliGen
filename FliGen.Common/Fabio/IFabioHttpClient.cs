using System.Threading.Tasks;

namespace FliGen.Common.Fabio
{
    public interface IFabioHttpClient
    {
        Task<T> GetAsync<T>(string requestUri);
    }
}