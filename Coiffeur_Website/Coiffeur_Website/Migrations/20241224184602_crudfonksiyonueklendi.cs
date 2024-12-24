using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Coiffeur_Website.Migrations
{
    public partial class crudfonksiyonueklendi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calisanlar_Salonlar_SalonId",
                table: "Calisanlar");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Calisanlar_CalisanId",
                table: "Randevular");

            migrationBuilder.AlterColumn<int>(
                name: "SalonId",
                table: "Calisanlar",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Calisanlar_Salonlar_SalonId",
                table: "Calisanlar",
                column: "SalonId",
                principalTable: "Salonlar",
                principalColumn: "SalonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Calisanlar_CalisanId",
                table: "Randevular",
                column: "CalisanId",
                principalTable: "Calisanlar",
                principalColumn: "CalisanId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calisanlar_Salonlar_SalonId",
                table: "Calisanlar");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Calisanlar_CalisanId",
                table: "Randevular");

            migrationBuilder.AlterColumn<int>(
                name: "SalonId",
                table: "Calisanlar",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Calisanlar_Salonlar_SalonId",
                table: "Calisanlar",
                column: "SalonId",
                principalTable: "Salonlar",
                principalColumn: "SalonId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Calisanlar_CalisanId",
                table: "Randevular",
                column: "CalisanId",
                principalTable: "Calisanlar",
                principalColumn: "CalisanId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
