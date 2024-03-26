using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wshop3.Migrations
{
    /// <inheritdoc />
    public partial class redeles_termek : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "kosar");

            migrationBuilder.RenameIndex(
                name: "felhasznalo_id1",
                table: "rendelesek",
                newName: "felhasznalo_id");

            migrationBuilder.CreateTable(
                name: "rendeles_termek",
                columns: table => new
                {
                    rendeles_id = table.Column<int>(type: "int(11)", nullable: false),
                    termek_id = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.rendeles_id, x.termek_id })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                    table.ForeignKey(
                        name: "rendeles_termek_ibfk_1",
                        column: x => x.rendeles_id,
                        principalTable: "rendelesek",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "rendeles_termek_ibfk_2",
                        column: x => x.termek_id,
                        principalTable: "termekek",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_hungarian_ci");

            migrationBuilder.CreateIndex(
                name: "termek_id",
                table: "rendeles_termek",
                column: "termek_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rendeles_termek");

            migrationBuilder.RenameIndex(
                name: "felhasznalo_id",
                table: "rendelesek",
                newName: "felhasznalo_id1");

            migrationBuilder.CreateTable(
                name: "kosar",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    felhasznalo_id = table.Column<int>(type: "int(11)", nullable: true),
                    termek_id = table.Column<int>(type: "int(11)", nullable: true),
                    mennyiseg = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "kosar_ibfk_1",
                        column: x => x.felhasznalo_id,
                        principalTable: "felhasznalok",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "kosar_ibfk_2",
                        column: x => x.termek_id,
                        principalTable: "termekek",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_hungarian_ci");

            migrationBuilder.CreateIndex(
                name: "felhasznalo_id",
                table: "kosar",
                column: "felhasznalo_id");

            migrationBuilder.CreateIndex(
                name: "termek_id",
                table: "kosar",
                column: "termek_id");
        }
    }
}
