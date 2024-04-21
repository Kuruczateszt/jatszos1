using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wshop3.Migrations
{
    /// <inheritdoc />
    public partial class rendelesek_mod1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "rendelesek_ibfk_2",
                table: "rendelesek");

            migrationBuilder.DropIndex(
                name: "termek_id1",
                table: "rendelesek");

            migrationBuilder.DropColumn(
                name: "mennyiseg",
                table: "rendelesek");

            migrationBuilder.DropColumn(
                name: "termek_id",
                table: "rendelesek");

            migrationBuilder.AlterColumn<DateTime>(
                name: "rendeles_ideje",
                table: "rendelesek",
                type: "timestamp",
                nullable: true,
                defaultValueSql: "current_timestamp()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "rendelesek",
                keyColumn: "felhasznalo_id",
                keyValue: null,
                column: "felhasznalo_id",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "felhasznalo_id",
                table: "rendelesek",
                type: "varchar(255)",
                nullable: false,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(int),
                oldType: "int(11)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "mennyiseg",
                table: "rendeles_termek",
                type: "int(11)",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "mennyiseg",
                table: "rendeles_termek");

            migrationBuilder.AlterColumn<DateTime>(
                name: "rendeles_ideje",
                table: "rendelesek",
                type: "timestamp",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp",
                oldNullable: true,
                oldDefaultValueSql: "current_timestamp()");

            migrationBuilder.AlterColumn<int>(
                name: "felhasznalo_id",
                table: "rendelesek",
                type: "int(11)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldCollation: "utf8mb4_general_ci")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "mennyiseg",
                table: "rendelesek",
                type: "int(11)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "termek_id",
                table: "rendelesek",
                type: "int(11)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "termek_id1",
                table: "rendelesek",
                column: "termek_id");

            migrationBuilder.AddForeignKey(
                name: "rendelesek_ibfk_2",
                table: "rendelesek",
                column: "termek_id",
                principalTable: "termekek",
                principalColumn: "id");
        }
    }
}
