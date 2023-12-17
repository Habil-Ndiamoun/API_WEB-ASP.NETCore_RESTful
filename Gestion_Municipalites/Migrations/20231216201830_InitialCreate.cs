using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion_Municipalites.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Municipalites",
                columns: table => new
                {
                    MunicipaliteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomMunicipalite = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AdresseCourriel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AdresseWeb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateProchaineElection = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Actif = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipalites", x => x.MunicipaliteId);
                });

            migrationBuilder.CreateTable(
                name: "Elections",
                columns: table => new
                {
                    ElectionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodeGeographique = table.Column<int>(type: "int", nullable: false),
                    DateElections = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MunicipaliteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Elections", x => x.ElectionId);
                    table.ForeignKey(
                        name: "FK_Elections_Municipalites_MunicipaliteId",
                        column: x => x.MunicipaliteId,
                        principalTable: "Municipalites",
                        principalColumn: "MunicipaliteId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Elections_MunicipaliteId",
                table: "Elections",
                column: "MunicipaliteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Elections");

            migrationBuilder.DropTable(
                name: "Municipalites");
        }
    }
}
