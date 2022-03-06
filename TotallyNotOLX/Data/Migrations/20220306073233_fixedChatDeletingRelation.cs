using Microsoft.EntityFrameworkCore.Migrations;

namespace TotallyNotOLX.Data.Migrations
{
    public partial class fixedChatDeletingRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Products_ProductID",
                table: "Chats");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Products_ProductID",
                table: "Chats",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Products_ProductID",
                table: "Chats");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Products_ProductID",
                table: "Chats",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
