using FliGen.Services.Players.Persistence.Helper;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FliGen.Services.Players.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            InitialFill.NamesAndRatesFill(migrationBuilder);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerRate");

            migrationBuilder.DropTable(
                name: "Player");
        }
    }
}
