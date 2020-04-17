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
        public MockedData MockedDataInstance { get; set; }

        public PlayersContext Context { get; set; }
        public string ConnectionString { get; set; }
            = "Server=(localdb)\\mssqllocaldb;Database=FliGen.Players.Test;Trusted_Connection=True;MultipleActiveResultSets=true";

        public TestDbFixture()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlServer()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<PlayersContext>();

            builder.UseSqlServer(ConnectionString)
                .UseInternalServiceProvider(serviceProvider);
            InitDb(builder.Options);
        }

        public void InitDb(DbContextOptions<PlayersContext> options)
        {
            Context = new PlayersContext(options);
            Context.Database.Migrate();

            MockedDataInstance = new MockedData()
            {
                PlayerExternalIdForDelete = Guid.NewGuid().ToString(),
            };
            var playerForDelete = Player.Create("for delete", "for delete", externalId: MockedDataInstance.PlayerExternalIdForDelete);
            var entity = Context.Add(playerForDelete);
            Context.SaveChanges();
            MockedDataInstance.PlayerInternalIdForDelete = entity.Entity.Id;
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

        public async Task CheckIfPlayerExists(int internalId, TaskCompletionSource<bool> receivedTask)
        {
            try
            {
                var entity = await Context.Players.SingleOrDefaultAsync(p => p.Id == internalId);

                if (entity is null)
                {
                    receivedTask.TrySetResult(false);
                }
                receivedTask.TrySetResult(true);
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

        public class MockedData
        {
            public string PlayerExternalIdForDelete { get; set; }
            public int PlayerInternalIdForDelete { get; set; }
        }
    }


}