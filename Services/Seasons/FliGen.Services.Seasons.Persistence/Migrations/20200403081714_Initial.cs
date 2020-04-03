using FliGen.Common.Sql;
using FliGen.Services.Seasons.Persistence.Helper;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FliGen.Services.Seasons.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            InitialFill.SeasonsFill(migrationBuilder);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Season");
        }
    }
}
