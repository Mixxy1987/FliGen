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
            InitialFillTeams.TeamsFill(migrationBuilder);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaguePlayerLinks");

            migrationBuilder.DropTable(
                name: "LeaguePlayerRole");

            migrationBuilder.DropTable(
                name: "LeagueSeasonLinks");

            migrationBuilder.DropTable(
                name: "LeagueSettings");

            migrationBuilder.DropTable(
                name: "LeagueType");

            migrationBuilder.DropTable(
                name: "PlayerRate");

            migrationBuilder.DropTable(
                name: "PlayerRatePlayerLinks");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "TeamPlayerLinks");

            migrationBuilder.DropTable(
                name: "TeamRole");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "Tour");

            migrationBuilder.DropTable(
                name: "Season");

            migrationBuilder.DropTable(
                name: "League");
        }
    }
}
