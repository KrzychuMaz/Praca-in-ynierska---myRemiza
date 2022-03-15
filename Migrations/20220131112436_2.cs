using Microsoft.EntityFrameworkCore.Migrations;

namespace myRemiza.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SamochodyId2",
                schema: "Identity",
                table: "Wyjazdy",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SamochodyId2",
                schema: "Identity",
                table: "Wyjazdy");
        }
    }
}
