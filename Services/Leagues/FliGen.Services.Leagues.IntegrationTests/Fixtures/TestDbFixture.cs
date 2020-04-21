using FliGen.Services.Leagues.Domain.Entities;
using FliGen.Services.Leagues.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using FliGen.Services.Leagues.Domain.Entities.Enum;

namespace FliGen.Services.Leagues.IntegrationTests.Fixtures
{
    public class TestDbFixture : IDisposable
    {
        private const string ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=FliGen.Leagues.Test;Trusted_Connection=True;MultipleActiveResultSets=true";
        private LeaguesContext Context { get; set; }
        
        public MockedData MockedDataInstance { get; private set; }

        public TestDbFixture()
        {
            CreateContextAndMigrateDb();
            InitDb();
        }

        public void InitDb()
        {
            var leagueForDelete = League.Create("for delete", "descr", LeagueType.Football);
            var leagueForUpdate = League.Create("for update", "descr", LeagueType.Football);
           
            var entityForDelete = Context.Leagues.Add(leagueForDelete);
            var entityForUpdate = Context.Leagues.Add(leagueForUpdate);

            Context.SaveChanges();
            MockedDataInstance = new MockedData
            {
                LeagueForDeleteId = entityForDelete.Entity.Id,
                LeagueForUpdateId = entityForUpdate.Entity.Id
            };

            var leagueSettingsForUpdate = LeagueSettings.Create(false, false, leagueId: entityForUpdate.Entity.Id, 10, 50);
            Context.LeagueSettings.Add(leagueSettingsForUpdate);
            Context.SaveChanges();
        }

        public async Task GetLeagueById(int id, TaskCompletionSource<League> receivedTask)
        {
            using (var context = CreateContext())
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
            using (var context = CreateContext())
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
            Context.Database.EnsureDeleted();
        }

        private void CreateContextAndMigrateDb()
        {
            Context = new LeaguesContext(CreateNewContextOptions());
            Context.Database.Migrate();
        }

        private static LeaguesContext CreateContext()
        {
            return new LeaguesContext(CreateNewContextOptions());
        }

        private static DbContextOptions<LeaguesContext> CreateNewContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlServer()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<LeaguesContext>();

            builder.UseSqlServer(ConnectionString)
                .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }
    }
}