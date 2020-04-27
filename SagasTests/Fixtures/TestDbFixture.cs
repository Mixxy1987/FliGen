using Microsoft.EntityFrameworkCore;
using System;
using FliGen.Services.Leagues.Persistence.Contexts;
using FliGen.Services.Tours.Persistence.Contexts;
using LeaguesContextFactory = FliGen.Services.Leagues.IntegrationTests.Fixtures.LeaguesContextFactory;
using ToursContextFactory = FliGen.Services.Tours.IntegrationTests.Fixtures.ToursContextFactory;

namespace SagasTests.Fixtures
{
    public class TestDbFixture : IDisposable
    {
        public ToursContextFactory ToursContextFactory { get; }
        public LeaguesContextFactory LeaguesContextFactory { get; }

        private ToursContext ToursContext { get; }
        private LeaguesContext LeaguesContext { get; }

        public MockedData MockedDataInstance { get; private set; }

        public TestDbFixture()
        {
            ToursContextFactory = new ToursContextFactory(
                "Server=(localdb)\\mssqllocaldb;Database=FliGen.Tours.Test;Trusted_Connection=True;MultipleActiveResultSets=true");

            ToursContext = GetInitiatedToursContext();

            ToursContextFactory = new ToursContextFactory(
                "Server=(localdb)\\mssqllocaldb;Database=FliGen.Leagues.Test;Trusted_Connection=True;MultipleActiveResultSets=true");

            ToursContext = GetInitiatedToursContext();
            LeaguesContext = GetInitiatedLeaguesContext();
        }

        public ToursContext GetInitiatedToursContext()
        {
            ToursContext context = CreateToursContextAndMigrateDb();
            

            return context;
        }

        public LeaguesContext GetInitiatedLeaguesContext()
        {
            LeaguesContext context = CreateLeaguesContextAndMigrateDb();


            return context;
        }

        public void Dispose()
        {
            ToursContext.Database.EnsureDeleted();
        }

        private LeaguesContext CreateLeaguesContextAndMigrateDb()
        {
            var context = LeaguesContextFactory.Create();
            context.Database.Migrate();
            return context;
        }

        private ToursContext CreateToursContextAndMigrateDb()
        {
            var context = ToursContextFactory.Create();
            context.Database.Migrate();
            return context;
        }
    }
}