using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IND.Migrations
{
    /// <inheritdoc />
    public partial class third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ElevInfos",
                columns: table => new
                {
                    ElevID = table.Column<int>(type: "int", nullable: false),
                    Namn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Klass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BetygValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KursNamn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LarareNamn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LonBelopp = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElevInfos");
        }
    }
}
