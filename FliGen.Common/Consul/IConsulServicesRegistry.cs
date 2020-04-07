using Consul;
using System.Threading.Tasks;

namespace FliGen.Common.Consul
{
    public interface IConsulServicesRegistry
    {
        Task<AgentService> GetAsync(string name);
    }
}