using System.Threading.Tasks;
using RestEase;


namespace FliGen.Services.Api.Services
{
    [SerializationMethods(Query = QuerySerializationMethod.Serialized)]
    public interface ITeamsService
    {
        [AllowAnyStatusCode]
        [Get("Ping")]
        Task Ping();
    }
}