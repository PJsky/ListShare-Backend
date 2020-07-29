using Microsoft.EntityFrameworkCore.Migrations;

namespace ListCoreApp.Migrations
{
    public partial class listPassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccessCode",
                table: "ItemLists",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ListPassword",
                table: "ItemLists",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessCode",
                table: "ItemLists");

            migrationBuilder.DropColumn(
                name: "ListPassword",
                table: "ItemLists");
        }
    }
}
