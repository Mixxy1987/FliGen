using System.Threading.Tasks;

namespace FliGen.Common.Mongo
{
    public interface IMongoDbSeeder
    {
        Task SeedAsync();
    }
}