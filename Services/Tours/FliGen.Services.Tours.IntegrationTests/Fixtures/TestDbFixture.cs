using FliGen.Services.Tours.Domain.Entities;
using FliGen.Services.Tours.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace FliGen.Services.Tours.IntegrationTests.Fixtures
{
    public class TestDbFixture : IDisposable
    {
        public MockedData MockedDataInstance { get; private set; }

        public ToursContextFactory ToursContextFactory { get; }
        private ToursContext Context { get; }


        public TestDbFixture()
        {
            ToursContextFactory = new ToursContextFactory(
                "Server=(localdb)\\mssqllocaldb;Database=FliGen.Tours.Test;Trusted_Connection=True;MultipleActiveResultSets=true");
            Context = GetInitiatedToursContext();
        }

        public ToursContext GetInitiatedToursContext()
        {
            ToursContext context = CreateContextAndMigrateDb();
            var tourForCancel = Tour.Create(DateTime.UtcNow.AddDays(2), 10);
            var tourForOpen = Tour.Create(DateTime.UtcNow.AddDays(3), 10);
            
            var tourForReopen = Tour.Create(DateTime.UtcNow.AddDays(4), 10);
            tourForReopen.MoveTourStatusForward();
            tourForReopen.MoveTourStatusForward();
            var tourForBack = Tour.Create(DateTime.UtcNow.AddDays(5), 10);
            var tourForReadById = Tour.Create(DateTime.UtcNow.AddDays(6), 15);
            tourForReadById.MoveTourStatusForward();

            var entityForCancel = Context.Tours.Add(tourForCancel);
            var entityForOpen = Context.Tours.Add(tourForOpen);
            var entityForReopen = Context.Tours.Add(tourForReopen);
            var entityForBack = Context.Tours.Add(tourForBack);
            var entityForReadById = Context.Tours.Add(tourForReadById);


            context.SaveChanges();

            MockedDataInstance = new MockedData
            {
                TourForReadById = tourForReadById,
                TourForCancelId = entityForCancel.Entity.Id,
                TourForOpenId = entityForOpen.Entity.Id,
                TourForReopenId = entityForReopen.Entity.Id,
                TourForBackId = entityForBack.Entity.Id,
            };
            MockedDataInstance.TourForReadById.Id = entityForReadById.Entity.Id;
            return context;
        }

        public async Task GetTourById(int id, TaskCompletionSource<Tour> receivedTask)
        {
            using (var context = ToursContextFactory.Create())
            {
                try
                {
                    var entity = await context.Tours.SingleOrDefaultAsync(t => t.Id == id);

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

        public async Task GetTourByDate(string date, TaskCompletionSource<Tour> receivedTask)
        {
            if (string.IsNullOrWhiteSpace(date))
            {
                throw new ArgumentNullException(nameof(date));
            }

            using (var context = ToursContextFactory.Create())
            {
                try
                {
                    DateTime dt = DateTime.Parse(date);
                    Tour tourEntity = await context.Tours.SingleAsync(t => t.Date == dt);
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
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
        }

        private ToursContext CreateContextAndMigrateDb()
        {
            var context = ToursContextFactory.Create();
            context.Database.Migrate();
            return context;
        }
    }
}