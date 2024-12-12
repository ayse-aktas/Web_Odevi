using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coiffeur_Website.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FotografAdres",
                table: "Musteriler");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FotografAdres",
                table: "Musteriler",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
