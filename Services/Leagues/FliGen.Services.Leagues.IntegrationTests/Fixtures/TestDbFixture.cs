using FliGen.Services.Leagues.Domain.Entities;
using FliGen.Services.Leagues.Domain.Entities.Enum;
using FliGen.Services.Leagues.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FliGen.Services.Leagues.IntegrationTests.Fixtures
{
    public class TestDbFixture : IDisposable
    {
        public LeaguesContextFactory LeaguesContextFactory { get; }
        private LeaguesContext LeaguesContext { get; }
        
        public MockedData MockedDataInstance { get; private set; }

        public TestDbFixture()
        {
            LeaguesContextFactory = new LeaguesContextFactory(
                "Server=(localdb)\\mssqllocaldb;Database=FliGen.Leagues.Test;Trusted_Connection=True;MultipleActiveResultSets=true");

            LeaguesContext = GetInitiatedLeaguesContext();
        }

        public LeaguesContext GetInitiatedLeaguesContext()
        {
            LeaguesContext context = CreateContextAndMigrateDb();

            var leagueForDelete = League.Create("for delete", "descr", LeagueType.Football);
            var leagueForUpdate = League.Create("for update", "descr", LeagueType.Football);
            var leagueForJoin1 = League.Create("for join 1", "descr", LeagueType.Football);
            var leagueForJoin2 = League.Create("for join 2", "descr", LeagueType.Hockey);
            var leagueForJoin3 = League.Create("for join 3", "descr", LeagueType.Hockey);

            var entityForDelete = context.Leagues.Add(leagueForDelete);
            var entityForUpdate = context.Leagues.Add(leagueForUpdate);
            var entityForJoin1 = context.Leagues.Add(leagueForJoin1);
            var entityForJoin2 = context.Leagues.Add(leagueForJoin2);
            var entityForJoin3 = context.Leagues.Add(leagueForJoin3);

            context.SaveChanges();
            MockedDataInstance = new MockedData
            {
                LeagueForDeleteId = entityForDelete.Entity.Id,
                LeagueForUpdateId = entityForUpdate.Entity.Id,
                LeagueForJoinId1 = entityForJoin1.Entity.Id,
                LeagueForJoinId2 = entityForJoin2.Entity.Id,
                LeagueForJoinId3 = entityForJoin3.Entity.Id,
                PlayersInTeam = 10,
                TeamsInTour = 50,
                Player1 = 100,
                Player2 = 101,
                Player3 = 102,
                Player4 = 103,
                Player5 = 104
            };

            IEnumerable<LeaguePlayerLink> links = new[]
            {
                // 1 league
                LeaguePlayerLink.CreateJoinedLink(MockedDataInstance.LeagueForJoinId1, MockedDataInstance.Player1),
                LeaguePlayerLink.CreateJoinedLink(MockedDataInstance.LeagueForJoinId1, MockedDataInstance.Player2),
                LeaguePlayerLink.CreateWaitingLink(MockedDataInstance.LeagueForJoinId1, MockedDataInstance.Player3),
                // 2 league
                LeaguePlayerLink.CreateJoinedLink(MockedDataInstance.LeagueForJoinId2, MockedDataInstance.Player2),
                LeaguePlayerLink.CreateWaitingLink(MockedDataInstance.LeagueForJoinId2, MockedDataInstance.Player3),
                LeaguePlayerLink.CreateWaitingLink(MockedDataInstance.LeagueForJoinId2, MockedDataInstance.Player4),
                LeaguePlayerLink.CreateWaitingLink(MockedDataInstance.LeagueForJoinId2, MockedDataInstance.Player5),
                // 3 league
                LeaguePlayerLink.CreateJoinedLink(MockedDataInstance.LeagueForJoinId3, MockedDataInstance.Player1),
                LeaguePlayerLink.CreateJoinedLink(MockedDataInstance.LeagueForJoinId3, MockedDataInstance.Player2),
                LeaguePlayerLink.CreateJoinedLink(MockedDataInstance.LeagueForJoinId3, MockedDataInstance.Player3),
                LeaguePlayerLink.CreateJoinedLink(MockedDataInstance.LeagueForJoinId3, MockedDataInstance.Player4),
                LeaguePlayerLink.CreateJoinedLink(MockedDataInstance.LeagueForJoinId3, MockedDataInstance.Player5)
            };
            MockedDataInstance.League1JoinedPlayersCount = 2;
            MockedDataInstance.League2JoinedPlayersCount = 1;
            MockedDataInstance.League3JoinedPlayersCount = 5;

            MockedDataInstance.League1WaitingPlayersCount = 1;
            MockedDataInstance.League1WaitingPlayersCount = 3;
            MockedDataInstance.League1WaitingPlayersCount = 0;

            context.LeaguePlayerLinks.AddRange(links);

            var leagueSettingsForUpdate = LeagueSettings.Create(
                false, 
                false, 
                leagueId: entityForUpdate.Entity.Id,
                MockedDataInstance.PlayersInTeam, 
                MockedDataInstance.TeamsInTour);

            context.LeagueSettings.Add(leagueSettingsForUpdate);
            context.SaveChanges();
            return context;
        }

        public async Task GetLeagueById(int id, TaskCompletionSource<League> receivedTask)
        {
            await using(var context = LeaguesContextFactory.Create())
            {
                try
                {
                    var entity = await context.Leagues
                        .Include(l => l.LeagueSettings)
                        .SingleOrDefaultAsync(l => l.Id == id);

                    receivedTask.TrySetResult(entity);
                }
                catch (Exception e)
                {
                    receivedTask.TrySetException(e);
                }
            }
        }

        public async Task GetLeagueByName(string name, TaskCompletionSource<League> receivedTask)
        {
            await using(var context = LeaguesContextFactory.Create())
            {
                try
                {
                    var entity = await context.Leagues
                        .SingleOrDefaultAsync(l => l.Name == name);

                    if (entity is null)
                    {
                        receivedTask.TrySetCanceled();
                    }
                    receivedTask.TrySetResult(entity);
                }
                catch (Exception e)
                {
                    receivedTask.TrySetException(e);
                }
            }
        }
        public void Dispose()
        {
            LeaguesContext?.Database.EnsureDeleted();
        }

        private LeaguesContext CreateContextAndMigrateDb()
        {
            var leaguesContext = LeaguesContextFactory.Create();
            leaguesContext.Database.Migrate();
            return leaguesContext;
        }
    }
}