using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class barcodeField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SeoUrl",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "Product");

            migrationBuilder.AddColumn<string>(
                name: "Barcode",
                table: "Product",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Barcode",
                table: "Product");

            migrationBuilder.AddColumn<string>(
                name: "SeoUrl",
                table: "Product",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "Product",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }
    }
}
