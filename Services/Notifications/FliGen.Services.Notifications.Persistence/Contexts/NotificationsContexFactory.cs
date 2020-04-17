using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FliGen.Services.Notifications.Persistence.Contexts
{
    public class NotificationsContextFactory : IDesignTimeDbContextFactory<NotificationsContext>
    {
        public NotificationsContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<NotificationsContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb; Database=FliGen.Notifications; Trusted_Connection=True; MultipleActiveResultSets=true");
            return new NotificationsContext(optionsBuilder.Options);
        }
    }
}
