using Microsoft.EntityFrameworkCore.Migrations;

namespace FullStack.API.Migrations
{
    public partial class TableKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mobiles_Brands_BrandId",
                table: "Mobiles");

            migrationBuilder.AlterColumn<int>(
                name: "BrandId",
                table: "Mobiles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Mobiles_Brands_BrandId",
                table: "Mobiles",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mobiles_Brands_BrandId",
                table: "Mobiles");

            migrationBuilder.AlterColumn<int>(
                name: "BrandId",
                table: "Mobiles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Mobiles_Brands_BrandId",
                table: "Mobiles",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
