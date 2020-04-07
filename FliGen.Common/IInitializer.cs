using System.Threading.Tasks;

namespace FliGen.Common
{
    public interface IInitializer
    {
        Task InitializeAsync();
    }
}