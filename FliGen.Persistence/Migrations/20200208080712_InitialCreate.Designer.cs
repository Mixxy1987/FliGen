﻿// <auto-generated />
using System;
using FliGen.Persistence.Contextes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FliGen.Persistence.Migrations
{
    [DbContext(typeof(FliGenContext))]
    [Migration("20200208080712_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Fligen.Domain.Entities.League", b =>
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

                    b.ToTable("League");
                });

            modelBuilder.Entity("Fligen.Domain.Entities.LeagueSeasonLink", b =>
                {
                    b.Property<int>("SeasonId")
                        .HasColumnType("int");

                    b.Property<int>("LeagueId")
                        .HasColumnType("int");

                    b.HasKey("SeasonId", "LeagueId");

                    b.ToTable("LeagueSeasonLinks");
                });

            modelBuilder.Entity("Fligen.Domain.Entities.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<int>("PlayerRateId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Player");
                });

            modelBuilder.Entity("Fligen.Domain.Entities.PlayerRate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<double>("Rate")
                        .HasColumnType("float")
                        .HasMaxLength(3);

                    b.HasKey("Id");

                    b.ToTable("PlayerRate");
                });

            modelBuilder.Entity("Fligen.Domain.Entities.PlayerRatePlayerLink", b =>
                {
                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("PlayerRateId")
                        .HasColumnType("int");

                    b.HasKey("PlayerId", "PlayerRateId");

                    b.ToTable("PlayerRatePlayerLinks");
                });

            modelBuilder.Entity("Fligen.Domain.Entities.Season", b =>
                {
                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("End")
                        .HasColumnType("datetime2");

                    b.HasKey("Start", "End");

                    b.ToTable("Season");
                });

            modelBuilder.Entity("Fligen.Domain.Entities.SeasonTourLink", b =>
                {
                    b.Property<int>("SeasonId")
                        .HasColumnType("int");

                    b.Property<int>("TourId")
                        .HasColumnType("int");

                    b.HasKey("SeasonId", "TourId");

                    b.ToTable("SeasonTourLinks");
                });

            modelBuilder.Entity("Fligen.Domain.Entities.Team", b =>
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

            modelBuilder.Entity("Fligen.Domain.Entities.TeamPlayerLink", b =>
                {
                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("PlayerId", "TeamId");

                    b.ToTable("TeamPlayerLinks");
                });

            modelBuilder.Entity("Fligen.Domain.Entities.Tour", b =>
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

                    b.Property<DateTime>("TourDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Tour");
                });
#pragma warning restore 612, 618
        }
    }
}
