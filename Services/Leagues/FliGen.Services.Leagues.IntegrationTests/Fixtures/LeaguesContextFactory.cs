using FliGen.Common;
using FliGen.Services.Leagues.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FliGen.Services.Leagues.IntegrationTests.Fixtures
{
    public class LeaguesContextFactory : IFactory<LeaguesContext>
    {
        private readonly string _connectionString;

        public LeaguesContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public LeaguesContext Create()
        {
            return new LeaguesContext(CreateContextOptions());
        }

        private DbContextOptions<LeaguesContext> CreateContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlServer()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<LeaguesContext>();

            builder.UseSqlServer(_connectionString)
                .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }
    }
}