using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coiffeur_Website.Migrations
{
    public partial class randevuUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Musteriler_MusteriId",
                table: "Randevular");

            migrationBuilder.AlterColumn<int>(
                name: "MusteriId",
                table: "Randevular",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Musteriler_MusteriId",
                table: "Randevular",
                column: "MusteriId",
                principalTable: "Musteriler",
                principalColumn: "MusteriId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Musteriler_MusteriId",
                table: "Randevular");

            migrationBuilder.AlterColumn<int>(
                name: "MusteriId",
                table: "Randevular",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Musteriler_MusteriId",
                table: "Randevular",
                column: "MusteriId",
                principalTable: "Musteriler",
                principalColumn: "MusteriId");
        }
    }
}
