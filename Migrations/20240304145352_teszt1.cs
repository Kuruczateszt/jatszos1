using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wshop3.Migrations
{
    /// <inheritdoc />
    public partial class teszt1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "felhasznalok",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nev = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_hungarian_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    jelszo = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_hungarian_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_hungarian_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_hungarian_ci");

            migrationBuilder.CreateTable(
                name: "kategoriak",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nev = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_hungarian_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_hungarian_ci");

            migrationBuilder.CreateTable(
                name: "termek_kepek",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    termek_id = table.Column<int>(type: "int(11)", nullable: true),
                    kep = table.Column<byte[]>(type: "longblob", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_hungarian_ci");

            migrationBuilder.CreateTable(
                name: "termekek",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nev = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_hungarian_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ar = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    leiras = table.Column<string>(type: "text", nullable: true, collation: "utf8mb4_hungarian_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    kategoria_id = table.Column<int>(type: "int(11)", nullable: true),
                    termek_kep_id = table.Column<int>(type: "int(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "termekek_ibfk_1",
                        column: x => x.kategoria_id,
                        principalTable: "kategoriak",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "termekek_ibfk_2",
                        column: x => x.termek_kep_id,
                        principalTable: "termek_kepek",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_hungarian_ci");

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

            migrationBuilder.CreateTable(
                name: "rendelesek",
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
                        name: "rendelesek_ibfk_1",
                        column: x => x.felhasznalo_id,
                        principalTable: "felhasznalok",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "rendelesek_ibfk_2",
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

            migrationBuilder.CreateIndex(
                name: "felhasznalo_id1",
                table: "rendelesek",
                column: "felhasznalo_id");

            migrationBuilder.CreateIndex(
                name: "termek_id1",
                table: "rendelesek",
                column: "termek_id");

            migrationBuilder.CreateIndex(
                name: "termek_id2",
                table: "termek_kepek",
                column: "termek_id");

            migrationBuilder.CreateIndex(
                name: "kategoria_id",
                table: "termekek",
                column: "kategoria_id");

            migrationBuilder.CreateIndex(
                name: "termek_kep_id",
                table: "termekek",
                column: "termek_kep_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "kosar");

            migrationBuilder.DropTable(
                name: "rendelesek");

            migrationBuilder.DropTable(
                name: "felhasznalok");

            migrationBuilder.DropTable(
                name: "termekek");

            migrationBuilder.DropTable(
                name: "kategoriak");

            migrationBuilder.DropTable(
                name: "termek_kepek");
        }
    }
}
