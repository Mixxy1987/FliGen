using FliGen.Common;
using FliGen.Services.Notifications.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FliGen.Services.Notifications.IntegrationTests.Fixtures
{
    public class NotificationsContextFactory : IFactory<NotificationsContext>
    {
        private readonly string _connectionString;

        public NotificationsContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public NotificationsContext Create()
        {
            return new NotificationsContext(CreateContextOptions());
        }

        private DbContextOptions<NotificationsContext> CreateContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlServer()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<NotificationsContext>();

            builder.UseSqlServer(_connectionString)
                .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }
    }
}