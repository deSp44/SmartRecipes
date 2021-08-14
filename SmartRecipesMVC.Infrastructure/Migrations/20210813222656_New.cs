using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartRecipesMVC.Infrastructure.Migrations
{
    public partial class New : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Difficulties_DifficultyId1",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_DifficultyId1",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "DifficultyId1",
                table: "Recipes");

            migrationBuilder.AlterColumn<int>(
                name: "DifficultyId",
                table: "Recipes",
                type: "int",
                nullable: true,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_DifficultyId",
                table: "Recipes",
                column: "DifficultyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Difficulties_DifficultyId",
                table: "Recipes",
                column: "DifficultyId",
                principalTable: "Difficulties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Difficulties_DifficultyId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_DifficultyId",
                table: "Recipes");

            migrationBuilder.AlterColumn<short>(
                name: "DifficultyId",
                table: "Recipes",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DifficultyId1",
                table: "Recipes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_DifficultyId1",
                table: "Recipes",
                column: "DifficultyId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Difficulties_DifficultyId1",
                table: "Recipes",
                column: "DifficultyId1",
                principalTable: "Difficulties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
