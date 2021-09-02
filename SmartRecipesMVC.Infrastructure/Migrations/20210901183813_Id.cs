using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartRecipesMVC.Infrastructure.Migrations
{
    public partial class Id : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Recipes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_OwnerId",
                table: "Recipes",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_AspNetUsers_OwnerId",
                table: "Recipes",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_AspNetUsers_OwnerId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_OwnerId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");
        }
    }
}
