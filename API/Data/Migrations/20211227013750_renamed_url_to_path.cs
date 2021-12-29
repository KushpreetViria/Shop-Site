using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class renamed_url_to_path : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "ItemImage");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "ItemImage",
                newName: "ImagePath");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "ItemImage",
                newName: "Url");

            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "ItemImage",
                type: "TEXT",
                nullable: true);
        }
    }
}
