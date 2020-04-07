using System;
using FliGen.Common.Sql;
using FliGen.Services.Leagues.Persistence.Helper;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FliGen.Services.Leagues.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(MigrationHelpers.GetDynamicSqlFromFile(@"SqlScripts/InitialFill.sql"));
            InitialFill.LeaguePlayerLinksFill(migrationBuilder);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaguePlayerLinks");

            migrationBuilder.DropTable(
                name: "LeaguePlayerPriority");

            migrationBuilder.DropTable(
                name: "LeaguePlayerRole");

            migrationBuilder.DropTable(
                name: "LeagueSettings");

            migrationBuilder.DropTable(
                name: "LeagueType");

            migrationBuilder.DropTable(
                name: "League");
        }
    }
}
