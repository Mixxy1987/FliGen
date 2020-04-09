using System;
using FliGen.Common.Sql;
using FliGen.Services.Tours.Persistence.Helper;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FliGen.Services.Tours.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(MigrationHelpers.GetDynamicSqlFromFile(@"SqlScripts/InitialFill.sql"));
            InitialFill.ToursFill(migrationBuilder);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tour");

            migrationBuilder.DropTable(
                name: "TourRegistration");

            migrationBuilder.DropTable(
                name: "TourStatus");
        }
    }
}
