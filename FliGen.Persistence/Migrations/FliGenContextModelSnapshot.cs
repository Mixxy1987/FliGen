﻿// <auto-generated />
using System;
using FliGen.Persistence.Contextes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FliGen.Persistence.Migrations
{
    [DbContext(typeof(FliGenContext))]
    partial class FliGenContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FliGen.Domain.Entities.Enum.LeaguePlayerRole", b =>
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

                    b.ToTable("LeaguePlayerRole");
                });

            modelBuilder.Entity("FliGen.Domain.Entities.Enum.LeagueType", b =>
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

                    b.ToTable("LeagueType");
                });

            modelBuilder.Entity("FliGen.Domain.Entities.League", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int>("LeagueTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("League");
                });

            modelBuilder.Entity("FliGen.Domain.Entities.LeaguePlayerLink", b =>
                {
                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("LeagueId")
                        .HasColumnType("int");

                    b.Property<DateTime>("JoinTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("LeaguePlayerRoleId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LeaveTime")
                        .HasColumnType("datetime2");

                    b.HasKey("PlayerId", "LeagueId");

                    b.HasIndex("LeagueId");

                    b.ToTable("LeaguePlayerLinks");
                });

            modelBuilder.Entity("FliGen.Domain.Entities.LeagueSeasonLink", b =>
                {
                    b.Property<int>("SeasonId")
                        .HasColumnType("int");

                    b.Property<int>("LeagueId")
                        .HasColumnType("int");

                    b.HasKey("SeasonId", "LeagueId");

                    b.ToTable("LeagueSeasonLinks");
                });

            modelBuilder.Entity("FliGen.Domain.Entities.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ExternalId")
                        .HasColumnType("nvarchar(450)")
                        .HasMaxLength(450);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("Player");
                });

            modelBuilder.Entity("FliGen.Domain.Entities.PlayerRate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<double>("Value")
                        .HasColumnType("float")
                        .HasMaxLength(3);

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("PlayerRate");
                });

            modelBuilder.Entity("FliGen.Domain.Entities.PlayerRatePlayerLink", b =>
                {
                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("PlayerRateId")
                        .HasColumnType("int");

                    b.HasKey("PlayerId", "PlayerRateId");

                    b.ToTable("PlayerRatePlayerLinks");
                });

            modelBuilder.Entity("FliGen.Domain.Entities.Season", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Finish")
                        .HasColumnType("datetime2");

                    b.Property<int>("LeagueId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("LeagueId");

                    b.ToTable("Season");
                });

            modelBuilder.Entity("FliGen.Domain.Entities.SeasonTourLink", b =>
                {
                    b.Property<int>("SeasonId")
                        .HasColumnType("int");

                    b.Property<int>("TourId")
                        .HasColumnType("int");

                    b.HasKey("SeasonId", "TourId");

                    b.ToTable("SeasonTourLinks");
                });

            modelBuilder.Entity("FliGen.Domain.Entities.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Team");
                });

            modelBuilder.Entity("FliGen.Domain.Entities.TeamPlayerLink", b =>
                {
                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("PlayerId", "TeamId");

                    b.ToTable("TeamPlayerLinks");
                });

            modelBuilder.Entity("FliGen.Domain.Entities.Tour", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GuestCount")
                        .HasColumnType("int");

                    b.Property<int>("GuestTeamId")
                        .HasColumnType("int");

                    b.Property<int>("HomeCount")
                        .HasColumnType("int");

                    b.Property<int>("HomeTeamId")
                        .HasColumnType("int");

                    b.Property<int>("SeasonId")
                        .HasColumnType("int");

                    b.Property<DateTime>("TourDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("SeasonId");

                    b.ToTable("Tour");
                });

            modelBuilder.Entity("FliGen.Domain.Entities.LeaguePlayerLink", b =>
                {
                    b.HasOne("FliGen.Domain.Entities.League", "League")
                        .WithMany("LeaguePlayerLinks")
                        .HasForeignKey("LeagueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FliGen.Domain.Entities.Player", "Player")
                        .WithMany("LeaguePlayerLinks")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FliGen.Domain.Entities.PlayerRate", b =>
                {
                    b.HasOne("FliGen.Domain.Entities.Player", "Player")
                        .WithMany("Rates")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FliGen.Domain.Entities.Season", b =>
                {
                    b.HasOne("FliGen.Domain.Entities.League", "League")
                        .WithMany("Seasons")
                        .HasForeignKey("LeagueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FliGen.Domain.Entities.Tour", b =>
                {
                    b.HasOne("FliGen.Domain.Entities.Season", "Season")
                        .WithMany("Tours")
                        .HasForeignKey("SeasonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
