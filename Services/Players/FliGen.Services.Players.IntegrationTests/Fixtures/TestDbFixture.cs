using FliGen.Services.Players.Domain.Entities;
using FliGen.Services.Players.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FliGen.Services.Players.IntegrationTests.Fixtures
{
    public class TestDbFixture : IDisposable
    {
        public PlayersContextFactory PlayersContextFactory { get; }
        private PlayersContext PlayersContext { get; }
        
        public MockedData MockedDataInstance { get; private set; }

        public TestDbFixture()
        {
            PlayersContextFactory = new PlayersContextFactory(
                "Server=(localdb)\\mssqllocaldb;Database=FliGen.Players.Test;Trusted_Connection=True;MultipleActiveResultSets=true");
            PlayersContext = GetInitiatedPlayersContext();
        }

        public PlayersContext GetInitiatedPlayersContext()
        {
            PlayersContext context = CreateContextAndMigrateDb();
            MockedDataInstance = new MockedData
            {
                PlayerExternalIdForDelete = Guid.NewGuid().ToString(),
                PlayerExternalIdForUpdate = Guid.NewGuid().ToString()
            };

            var playerForDelete = Player.Create("for delete", "for delete", externalId: MockedDataInstance.PlayerExternalIdForDelete);
            var entityForDelete = context.Add(playerForDelete);

            var playerForUpdate = Player.Create("for update", "for update", externalId: MockedDataInstance.PlayerExternalIdForUpdate);
            var entityForUpdate = context.Add(playerForUpdate);

            MockedDataInstance.ExistingPlayer = Guid.NewGuid().ToString();
            MockedDataInstance.NotExistingPlayer = Guid.NewGuid().ToString();

            var existingPlayer = Player.Create("exist", "d", externalId: MockedDataInstance.ExistingPlayer);
            var entityOfExistingPlayer = context.Players.Add(existingPlayer);

            context.SaveChanges();
            MockedDataInstance.PlayerInternalIdForDelete = entityForDelete.Entity.Id;
            MockedDataInstance.PlayerInternalIdForUpdate = entityForUpdate.Entity.Id;
            MockedDataInstance.ExistingPlayerInternalId = entityOfExistingPlayer.Entity.Id;

            return context;
        }

        public async Task GetPlayerAndRateByExternalId(string externalId, TaskCompletionSource<(Player, PlayerRate)> receivedTask)
        {
            if (string.IsNullOrWhiteSpace(externalId))
            {
                throw new ArgumentNullException(nameof(externalId));
            }

            await using (var context = PlayersContextFactory.Create())
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

            await using(var context = PlayersContextFactory.Create())
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
            using (var context = PlayersContextFactory.Create())
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
            PlayersContext.Database.EnsureDeleted();
        }

        private PlayersContext CreateContextAndMigrateDb()
        {
            var leaguesContext = PlayersContextFactory.Create();
            leaguesContext.Database.Migrate();
            return leaguesContext;
        }
    }
}