using FliGen.Services.Players.Domain.Entities;
using FliGen.Services.Players.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

            Player p1 = Player.Create("p1", "p1");
            Player p2 = Player.Create("p2", "p2");
            Player p3 = Player.Create("p1", "p3");
            Player p4 = Player.Create("p4", "p4");
            Player p5 = Player.Create("p5", "p5");
            Player p6 = Player.Create("p6", "p6");
            Player p7 = Player.Create("p7", "p7");
            Player p8 = Player.Create("p8", "p8");
            Player p9 = Player.Create("p9", "p9");
            Player p10 = Player.Create("p10", "p10");

            var entityP1 = context.Add(p1);
            var entityP2 = context.Add(p2);
            var entityP3 = context.Add(p3);
            var entityP4 = context.Add(p4);
            var entityP5 = context.Add(p5);
            var entityP6 = context.Add(p6);
            var entityP7 = context.Add(p7);
            var entityP8 = context.Add(p8);
            var entityP9 = context.Add(p9);
            var entityP10 = context.Add(p10);

            context.SaveChanges();

            int league1Id = 100;
            int league2Id = 101;
            var rates = new List<PlayerRate>
            {
                new PlayerRate(DateTime.UtcNow, "5.0", entityP1.Entity.Id, league1Id),
                new PlayerRate(DateTime.UtcNow, "6.0", entityP1.Entity.Id, league2Id),
                new PlayerRate(DateTime.UtcNow, "4.0", entityP2.Entity.Id, league1Id),
                new PlayerRate(DateTime.UtcNow, "3.0", entityP2.Entity.Id, league2Id),
                new PlayerRate(DateTime.UtcNow, "5.4", entityP3.Entity.Id, league1Id),
                new PlayerRate(DateTime.UtcNow, "2.0", entityP3.Entity.Id, league2Id),
                new PlayerRate(DateTime.UtcNow, "8.0", entityP4.Entity.Id, league1Id),
                new PlayerRate(DateTime.UtcNow, "7.0", entityP4.Entity.Id, league2Id),
                new PlayerRate(DateTime.UtcNow, "1.0", entityP5.Entity.Id, league1Id),
                new PlayerRate(DateTime.UtcNow, "2.0", entityP5.Entity.Id, league2Id),
                new PlayerRate(DateTime.UtcNow, "3.2", entityP6.Entity.Id, league1Id),
                new PlayerRate(DateTime.UtcNow, "6.4", entityP6.Entity.Id, league2Id),
                new PlayerRate(DateTime.UtcNow, "6.2", entityP7.Entity.Id, league1Id),
                new PlayerRate(DateTime.UtcNow, "6.0", entityP7.Entity.Id, league2Id),
                new PlayerRate(DateTime.UtcNow, "4.4", entityP8.Entity.Id, league1Id),
                new PlayerRate(DateTime.UtcNow, "1.2", entityP8.Entity.Id, league2Id),
                new PlayerRate(DateTime.UtcNow, "3.2", entityP9.Entity.Id, league1Id),
                new PlayerRate(DateTime.UtcNow, "6.9", entityP9.Entity.Id, league2Id),
                new PlayerRate(DateTime.UtcNow, "8.0", entityP10.Entity.Id, league1Id),
                new PlayerRate(DateTime.UtcNow, "6.8", entityP10.Entity.Id, league2Id),
            };

            context.PlayerRates.AddRange(rates);

            context.SaveChanges();

            MockedDataInstance.PlayerInternalIdForDelete = entityForDelete.Entity.Id;
            MockedDataInstance.PlayerInternalIdForUpdate = entityForUpdate.Entity.Id;
            MockedDataInstance.ExistingPlayerInternalId = entityOfExistingPlayer.Entity.Id;

            MockedDataInstance.LeagueForFilter1 = league1Id;
            MockedDataInstance.PlayerForFilter1 = entityP1.Entity.Id;
            MockedDataInstance.PlayerForFilter2 = entityP2.Entity.Id;

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
            var context = PlayersContextFactory.Create();
            context.Database.Migrate();
            return context;
        }
    }
}