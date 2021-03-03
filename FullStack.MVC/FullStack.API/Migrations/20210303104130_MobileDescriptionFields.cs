using Microsoft.EntityFrameworkCore.Migrations;

namespace FullStack.API.Migrations
{
    public partial class MobileDescriptionFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BateryDescription",
                table: "Mobiles",
                type: "VARCHAR(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CamaraDescripcion",
                table: "Mobiles",
                type: "VARCHAR(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Mobiles",
                type: "VARCHAR(250)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ScreenDescription",
                table: "Mobiles",
                type: "VARCHAR(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StorageDescription",
                table: "Mobiles",
                type: "VARCHAR(100)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BateryDescription",
                table: "Mobiles");

            migrationBuilder.DropColumn(
                name: "CamaraDescripcion",
                table: "Mobiles");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Mobiles");

            migrationBuilder.DropColumn(
                name: "ScreenDescription",
                table: "Mobiles");

            migrationBuilder.DropColumn(
                name: "StorageDescription",
                table: "Mobiles");
        }
    }
}
