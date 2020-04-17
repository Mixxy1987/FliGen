﻿// <auto-generated />
using FliGen.Services.Notifications.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FliGen.Services.Notifications.Persistence.Migrations
{
    [DbContext(typeof(NotificationsContext))]
    partial class NotificationsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FliGen.Services.Notifications.Domain.Entities.Enum.NotificationType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("NotificationType");
                });

            modelBuilder.Entity("FliGen.Services.Notifications.Domain.Entities.PlayerNotificationLink", b =>
                {
                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("NotificationTypeId")
                        .HasColumnType("int");

                    b.HasKey("PlayerId", "NotificationTypeId");

                    b.ToTable("PlayerNotificationLinks");
                });
#pragma warning restore 612, 618
        }
    }
}
