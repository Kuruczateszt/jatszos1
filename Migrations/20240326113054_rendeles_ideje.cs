using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wshop3.Migrations
{
    /// <inheritdoc />
    public partial class rendeles_ideje : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "rendeles_ideje",
                table: "rendelesek",
                type: "timestamp",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "rendeles_ideje",
                table: "rendelesek");
        }
    }
}
