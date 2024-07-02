using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IND.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Elevers",
                columns: table => new
                {
                    ElevID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Namn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Klass = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Elevers", x => x.ElevID);
                });

            migrationBuilder.CreateTable(
                name: "Kursers",
                columns: table => new
                {
                    KursID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KursNamn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Aktiv = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kursers", x => x.KursID);
                });

            migrationBuilder.CreateTable(
                name: "Personals",
                columns: table => new
                {
                    PersonID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Namn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Befattning = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnstallningsDatum = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personals", x => x.PersonID);
                });

            migrationBuilder.CreateTable(
                name: "Betygs",
                columns: table => new
                {
                    BetygID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ElevID = table.Column<int>(type: "int", nullable: false),
                    KursID = table.Column<int>(type: "int", nullable: false),
                    LarareID = table.Column<int>(type: "int", nullable: false),
                    BetygValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Betygs", x => x.BetygID);
                    table.ForeignKey(
                        name: "FK_Betygs_Elevers_ElevID",
                        column: x => x.ElevID,
                        principalTable: "Elevers",
                        principalColumn: "ElevID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Betygs_Kursers_KursID",
                        column: x => x.KursID,
                        principalTable: "Kursers",
                        principalColumn: "KursID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Betygs_Personals_LarareID",
                        column: x => x.LarareID,
                        principalTable: "Personals",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lons",
                columns: table => new
                {
                    LonID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonID = table.Column<int>(type: "int", nullable: false),
                    LonBelopp = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Avdelning = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lons", x => x.LonID);
                    table.ForeignKey(
                        name: "FK_Lons_Personals_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Personals",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Betygs_ElevID",
                table: "Betygs",
                column: "ElevID");

            migrationBuilder.CreateIndex(
                name: "IX_Betygs_KursID",
                table: "Betygs",
                column: "KursID");

            migrationBuilder.CreateIndex(
                name: "IX_Betygs_LarareID",
                table: "Betygs",
                column: "LarareID");

            migrationBuilder.CreateIndex(
                name: "IX_Lons_PersonID",
                table: "Lons",
                column: "PersonID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Betygs");

            migrationBuilder.DropTable(
                name: "Lons");

            migrationBuilder.DropTable(
                name: "Elevers");

            migrationBuilder.DropTable(
                name: "Kursers");

            migrationBuilder.DropTable(
                name: "Personals");
        }
    }
}
