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
        private readonly LeaguesContextFactory _leaguesContextFactory;
        private LeaguesContext LeaguesContext { get; set; }
        
        public MockedData MockedDataInstance { get; private set; }

        public TestDbFixture()
        {
            _leaguesContextFactory = new LeaguesContextFactory(
                "Server=(localdb)\\mssqllocaldb;Database=FliGen.Leagues.Test;Trusted_Connection=True;MultipleActiveResultSets=true");

            LeaguesContext = GetInitiatedLeaguesContext();
        }

        public LeaguesContext GetInitiatedLeaguesContext()
        {
            LeaguesContext leaguesContext = CreateContextAndMigrateDb();

            var leagueForDelete = League.Create("for delete", "descr", LeagueType.Football);
            var leagueForUpdate = League.Create("for update", "descr", LeagueType.Football);
            var leagueForJoin = League.Create("for join", "descr", LeagueType.Football);

            var entityForDelete = leaguesContext.Leagues.Add(leagueForDelete);
            var entityForUpdate = leaguesContext.Leagues.Add(leagueForUpdate);
            var entityForJoin = leaguesContext.Leagues.Add(leagueForJoin);

            leaguesContext.SaveChanges();
            MockedDataInstance = new MockedData
            {
                LeagueForDeleteId = entityForDelete.Entity.Id,
                LeagueForUpdateId = entityForUpdate.Entity.Id,
                LeagueForJoinId = entityForJoin.Entity.Id,
                PlayersInTeam = 10,
                TeamsInTour = 50,
            };
            int joinedPlayer1 = 1;
            int joinedPlayer2 = 2;
            int waitingPlayer3 = 3;
            MockedDataInstance.JoinedPlayersCount = 2;

            IEnumerable<LeaguePlayerLink> links = new[]
            {
                LeaguePlayerLink.CreateJoinedLink(MockedDataInstance.LeagueForJoinId, joinedPlayer1),
                LeaguePlayerLink.CreateJoinedLink(MockedDataInstance.LeagueForJoinId, joinedPlayer2),
                LeaguePlayerLink.CreateWaitingLink(MockedDataInstance.LeagueForJoinId, waitingPlayer3)
            };
            
            leaguesContext.LeaguePlayerLinks.AddRange(links);

            var leagueSettingsForUpdate = LeagueSettings.Create(
                false, 
                false, 
                leagueId: entityForUpdate.Entity.Id,
                MockedDataInstance.PlayersInTeam, 
                MockedDataInstance.TeamsInTour);

            leaguesContext.LeagueSettings.Add(leagueSettingsForUpdate);
            leaguesContext.SaveChanges();
            return leaguesContext;
        }

        public async Task GetLeagueById(int id, TaskCompletionSource<League> receivedTask)
        {
            using (var context = _leaguesContextFactory.Create())
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
            using (var context = _leaguesContextFactory.Create())
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
            var leaguesContext = _leaguesContextFactory.Create();
            leaguesContext.Database.Migrate();
            return leaguesContext;
        }
    }
}