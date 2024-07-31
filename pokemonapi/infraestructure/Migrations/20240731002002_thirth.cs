using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infraestructure.Migrations
{
    public partial class thirth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PokemondId",
                table: "Habilities");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PokemondId",
                table: "Habilities",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
