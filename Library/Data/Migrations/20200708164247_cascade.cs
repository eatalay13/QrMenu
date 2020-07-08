using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class cascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImage_Product",
                table: "ProductImage");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductToCategory_Category",
                table: "ProductToCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductToCategory_Product",
                table: "ProductToCategory");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImage_Product",
                table: "ProductImage",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductToCategory_Category",
                table: "ProductToCategory",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductToCategory_Product",
                table: "ProductToCategory",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImage_Product",
                table: "ProductImage");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductToCategory_Category",
                table: "ProductToCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductToCategory_Product",
                table: "ProductToCategory");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImage_Product",
                table: "ProductImage",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductToCategory_Category",
                table: "ProductToCategory",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductToCategory_Product",
                table: "ProductToCategory",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
