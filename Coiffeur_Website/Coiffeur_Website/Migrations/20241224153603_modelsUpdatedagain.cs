using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coiffeur_Website.Migrations
{
    public partial class modelsUpdatedagain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RandevuSaati",
                table: "Randevular",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RandevuSaati",
                table: "Randevular");
        }
    }
}
