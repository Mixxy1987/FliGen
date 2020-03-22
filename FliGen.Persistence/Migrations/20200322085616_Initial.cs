using System;
using FliGen.Persistence.Helper;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FliGen.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(MigrationHelpers.GetDynamicSqlFromFile(@"SqlScripts/InitialFill.sql"));
            InitialFillPlayers.NamesAndRatesFill(migrationBuilder);
            InitialFillPlayers.LeaguePlayerLinksFill(migrationBuilder);
            InitialFillSeasons.SeasonsAndToursFill(migrationBuilder);
            InitialFillTours.ToursFill(migrationBuilder);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
