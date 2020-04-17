using FliGen.Services.Notifications.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FliGen.Services.Notifications.Persistence.Configurations
{
    public class PlayerNotificationLinksConfiguration : IEntityTypeConfiguration<PlayerNotificationLink>
    {
        public void Configure(EntityTypeBuilder<PlayerNotificationLink> builder)
        {
            builder.ToTable("PlayerNotificationLinks");

            builder.Property(e => e.PlayerId)
                .IsRequired();

            builder.Property(e => e.NotificationTypeId)
                .IsRequired();

            builder.HasKey(e => new { e.PlayerId, e.NotificationTypeId });

            builder.Ignore(x => x.NotificationType);
        }
    }
}