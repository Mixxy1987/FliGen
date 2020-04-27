using FliGen.Services.Notifications.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;

namespace FliGen.Services.Notifications.IntegrationTests.Fixtures
{
    public class TestDbFixture : IDisposable
    {
        public MockedData MockedDataInstance { get; private set; }

        public NotificationsContextFactory ToursContextFactory { get; }
        private NotificationsContext Context { get; }


        public TestDbFixture()
        {
            ToursContextFactory = new NotificationsContextFactory(
                "Server=(localdb)\\mssqllocaldb;Database=FliGen.Notifications.Test;Trusted_Connection=True;MultipleActiveResultSets=true");
            Context = GetInitiatedToursContext();
        }

        public NotificationsContext GetInitiatedToursContext()
        {
            NotificationsContext context = CreateContextAndMigrateDb();
            
            return context;
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
        }

        private NotificationsContext CreateContextAndMigrateDb()
        {
            var context = ToursContextFactory.Create();
            context.Database.Migrate();
            return context;
        }
    }
}