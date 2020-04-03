using FliGen.Common.Sql;
using FliGen.Services.Teams.Persistence.Helper;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace FliGen.Services.Teams.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(MigrationHelpers.GetDynamicSqlFromFile(@"SqlScripts/InitialFill.sql"));

            int toursCount = ToursCount();
            InitialFillTeams.TeamsFill(migrationBuilder, toursCount);
            InitialFillTeamPlayers.TeamPlayersFill(migrationBuilder, toursCount * 2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "TeamPlayerLinks");

            migrationBuilder.DropTable(
                name: "TeamRole");
        }

        private int ToursCount()
        {
            int toursCount = 0;
            foreach (var date in InitialFillData.SeasonDates)
            {
                var currentTourDate = DateTime.Parse(date.Item2);
                for (int i = 0; i < InitialFillData.ToursInSeasonCount; i++)
                {
                    currentTourDate = currentTourDate.AddDays(7);
                    if (currentTourDate >= DateTime.Now)
                    {
                        break;
                    }

                    ++toursCount;
                }
            }

            return toursCount;
        }
    }
}