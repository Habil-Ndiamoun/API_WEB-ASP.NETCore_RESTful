using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion_Municipalites.Migrations
{
    /// <inheritdoc />
    public partial class MAJDuModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Elections_Municipalites_MunicipaliteId",
                table: "Elections");

            migrationBuilder.DropColumn(
                name: "CodeGeographique",
                table: "Elections");

            migrationBuilder.AlterColumn<int>(
                name: "MunicipaliteId",
                table: "Elections",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Elections_Municipalites_MunicipaliteId",
                table: "Elections",
                column: "MunicipaliteId",
                principalTable: "Municipalites",
                principalColumn: "MunicipaliteId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Elections_Municipalites_MunicipaliteId",
                table: "Elections");

            migrationBuilder.AlterColumn<int>(
                name: "MunicipaliteId",
                table: "Elections",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CodeGeographique",
                table: "Elections",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Elections_Municipalites_MunicipaliteId",
                table: "Elections",
                column: "MunicipaliteId",
                principalTable: "Municipalites",
                principalColumn: "MunicipaliteId");
        }
    }
}
