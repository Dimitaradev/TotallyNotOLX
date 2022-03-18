using Microsoft.EntityFrameworkCore.Migrations;

namespace TotallyNotOLX.Data.Migrations
{
    public partial class removedUnnecessaryProductFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceNegotiable",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Sold",
                table: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PriceNegotiable",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Sold",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
