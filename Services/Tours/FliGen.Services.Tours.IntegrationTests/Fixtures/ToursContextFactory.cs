using FliGen.Common;
using FliGen.Services.Tours.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FliGen.Services.Tours.IntegrationTests.Fixtures
{
    public class ToursContextFactory : IFactory<ToursContext>
    {
        private readonly string _connectionString;

        public ToursContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ToursContext Create()
        {
            return new ToursContext(CreateContextOptions());
        }

        private DbContextOptions<ToursContext> CreateContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlServer()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<ToursContext>();

            builder.UseSqlServer(_connectionString)
                .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }
    }
}