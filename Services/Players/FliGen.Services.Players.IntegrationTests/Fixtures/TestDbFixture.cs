using FliGen.Services.Players.Domain.Entities;
using FliGen.Services.Players.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FliGen.Services.Players.IntegrationTests.Fixtures
{
    public class TestDbFixture : IDisposable
    {
        private const string ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=FliGen.Players.Test;Trusted_Connection=True;MultipleActiveResultSets=true";
        private PlayersContext Context { get; set; }
        
        public MockedData MockedDataInstance { get; private set; }

        public TestDbFixture()
        {
            InitDb();
        }

        public void InitDb()
        {
            CreateContextAndMigrateDb();
            MockedDataInstance = new MockedData
            {
                PlayerExternalIdForDelete = Guid.NewGuid().ToString(),
                PlayerExternalIdForUpdate= Guid.NewGuid().ToString()
            };

            var playerForDelete = Player.Create("for delete", "for delete", externalId: MockedDataInstance.PlayerExternalIdForDelete);
            var entityForDelete = Context.Add(playerForDelete);

            var playerForUpdate = Player.Create("for update", "for update", externalId: MockedDataInstance.PlayerExternalIdForUpdate);
            var entityForUpdate = Context.Add(playerForUpdate);
            
            Context.SaveChanges();
            MockedDataInstance.PlayerInternalIdForDelete = entityForDelete.Entity.Id;
            MockedDataInstance.PlayerInternalIdForUpdate = entityForUpdate.Entity.Id;
        }

        public async Task GetPlayerAndRateByExternalId(string externalId, TaskCompletionSource<(Player, PlayerRate)> receivedTask)
        {
            if (string.IsNullOrWhiteSpace(externalId))
            {
                throw new ArgumentNullException(nameof(externalId));
            }

            using (var context = CreateContext())
            {
                try
                {
                    Player playerEntity = await context.Players.SingleAsync(p => p.ExternalId == externalId);
                    if (playerEntity is null)
                    {
                        receivedTask.TrySetCanceled();
                        return;
                    }

                    PlayerRate playerRateEntity =
                        context.PlayerRates
                            .Where(p => p.PlayerId == playerEntity.Id)
                            .OrderBy(p => p.Date)
                            .Last();
                    if (playerRateEntity is null)
                    {
                        receivedTask.TrySetCanceled();
                        return;
                    }
                    receivedTask.TrySetResult((playerEntity, playerRateEntity));
                }
                catch (Exception e)
                {
                    receivedTask.TrySetException(e);
                }
            }
        }

        public async Task GetPlayerByExternalId(string externalId, TaskCompletionSource<Player> receivedTask)
        {
            if (string.IsNullOrWhiteSpace(externalId))
            {
                throw new ArgumentNullException(nameof(externalId));
            }

            using (var context = CreateContext())
            {
                try
                {
                    Player entity = await context.Players.SingleAsync(p => p.ExternalId == externalId);

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
        }

        public async Task CheckIfPlayerExists(int internalId, TaskCompletionSource<bool> receivedTask)
        {
            using (var context = CreateContext())
            {
                try
                {
                    var entity = await context.Players.SingleOrDefaultAsync(p => p.Id == internalId);

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
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
        }

        private void CreateContextAndMigrateDb()
        {
            Context = new PlayersContext(CreateNewContextOptions());
            Context.Database.Migrate();
        }

        private static PlayersContext CreateContext()
        {
            return new PlayersContext(CreateNewContextOptions());
        }

        private static DbContextOptions<PlayersContext> CreateNewContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlServer()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<PlayersContext>();

            builder.UseSqlServer(ConnectionString)
                .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }
    }
}