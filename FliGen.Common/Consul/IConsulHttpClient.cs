using System.Threading.Tasks;

namespace FliGen.Common.Consul
{
    public interface IConsulHttpClient
    {
        Task<T> GetAsync<T>(string requestUri);
    }
}

