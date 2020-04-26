using FliGen.Common;
using FliGen.Services.Players.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FliGen.Services.Players.IntegrationTests.Fixtures
{
    public class PlayersContextFactory : IFactory<PlayersContext>
    {
        private readonly string _connectionString;

        public PlayersContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public PlayersContext Create()
        {
            return new PlayersContext(CreateContextOptions());
        }

        private DbContextOptions<PlayersContext> CreateContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlServer()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<PlayersContext>();

            builder.UseSqlServer(_connectionString)
                .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }
    }
}