using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartRecipesMVC.Infrastructure.Migrations
{
    public partial class ImageSplitTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ext",
                table: "Images",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ext",
                table: "Images");
        }
    }
}
