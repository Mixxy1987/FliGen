using FliGen.Services.Tours.Domain.Entities;
using FliGen.Services.Tours.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace FliGen.Services.Tours.IntegrationTests.Fixtures
{
    public class TestDbFixture : IDisposable
    {
        private const string ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=FliGen.Tours.Test;Trusted_Connection=True;MultipleActiveResultSets=true";
        private ToursContext Context { get; set; }
        
        public MockedData MockedDataInstance { get; private set; }

        public TestDbFixture()
        {
            CreateContext();
            InitDb();
        }

        public void InitDb()
        {
            MockedDataInstance = new MockedData()
            {

            };
            
            Context.SaveChanges();
        }

        public async Task CheckIfTourExists(int internalId, TaskCompletionSource<bool> receivedTask)
        {
            try
            {
                var entity = await Context.Tours.SingleOrDefaultAsync(p => p.Id == internalId);

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

        public async Task GetTourByDate(string date, TaskCompletionSource<Tour> receivedTask)
        {
            if (string.IsNullOrWhiteSpace(date))
            {
                throw new ArgumentNullException(nameof(date));
            }

            try
            {
                RecreateContext();
                DateTime dt = DateTime.Parse(date);
                Tour tourEntity = await Context.Tours.SingleAsync(p => p.Date == dt);
                if (tourEntity is null)
                {
                    receivedTask.TrySetCanceled();
                    return;
                }

                receivedTask.TrySetResult(tourEntity);
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

        private void RecreateContext()
        {
            Context.Dispose();
            CreateContext();
        }

        private void CreateContext()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlServer()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<ToursContext>();

            builder.UseSqlServer(ConnectionString)
                .UseInternalServiceProvider(serviceProvider);

            Context = new ToursContext(builder.Options);
            Context.Database.Migrate();
        }
    }
}