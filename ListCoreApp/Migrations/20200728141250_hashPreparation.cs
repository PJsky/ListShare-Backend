using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ListCoreApp.Migrations
{
    public partial class hashPreparation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "SecurityHash",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecurityHash",
                table: "Users");
        }
    }
}
