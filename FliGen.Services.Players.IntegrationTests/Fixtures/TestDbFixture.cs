using FliGen.Services.Players.Domain.Entities;
using FliGen.Services.Players.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace FliGen.Services.Players.IntegrationTests.Fixtures
{
    public class TestDbFixture : IDisposable
    {
        private bool _disposed = false;
        public PlayersContext Context;
        
        public string ConnectionString;

        public TestDbFixture()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlServer()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<PlayersContext>();

            ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=FliGen.Players.Test;Trusted_Connection=True;MultipleActiveResultSets=true";
            builder.UseSqlServer(ConnectionString)
                .UseInternalServiceProvider(serviceProvider);
            Context = new PlayersContext(builder.Options);
            Context.Database.Migrate();
        }

        public async Task GetPlayerByExternalId(string externalId, TaskCompletionSource<Player> receivedTask)
        {
            if (string.IsNullOrWhiteSpace(externalId))
            {
                throw new ArgumentNullException(nameof(externalId));
            }

            try
            {
                var entity = await Context.Players.SingleAsync(p => p.ExternalId == externalId);

                if (entity is null)
                {
                    receivedTask.TrySetCanceled();
                    return;
                }
                receivedTask.TrySetResult(entity);
            }
            catch (Exception e)
            {
                receivedTask.TrySetException(e);
            }
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
        }
    }
}