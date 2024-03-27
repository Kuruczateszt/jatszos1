using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wshop3.Migrations
{
    /// <inheritdoc />
    public partial class rendeles_termek3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "rendelesek_ibfk_1",
                table: "rendelesek");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "rendelesek_ibfk_1",
                table: "rendelesek",
                column: "felhasznalo_id",
                principalTable: "felhasznalok",
                principalColumn: "id");
        }
    }
}
