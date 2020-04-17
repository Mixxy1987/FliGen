using FliGen.Services.Notifications.Domain.Entities;
using FliGen.Services.Notifications.Domain.Entities.Enum;
using FliGen.Services.Notifications.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace FliGen.Services.Notifications.Persistence.Contexts
{
    public class NotificationsContext : DbContext
    {
        public DbSet<NotificationType> NotificationType { get; set; }
        public DbSet<PlayerNotificationLink> PlayerNotificationLinks { get; set; }

        public NotificationsContext(DbContextOptions<NotificationsContext> options) :base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(NotificationTypeConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
