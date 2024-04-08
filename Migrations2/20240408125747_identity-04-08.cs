using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wshop3.Migrations2
{
    /// <inheritdoc />
    public partial class identity0408 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "AspNetUsers",
                newName: "Varos");

            migrationBuilder.RenameColumn(
                name: "Age",
                table: "AspNetUsers",
                newName: "Iranyitoszam");

            migrationBuilder.AddColumn<int>(
                name: "Hazszam",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TeljesNev",
                table: "AspNetUsers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Utca",
                table: "AspNetUsers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hazszam",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TeljesNev",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Utca",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Varos",
                table: "AspNetUsers",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "Iranyitoszam",
                table: "AspNetUsers",
                newName: "Age");
        }
    }
}
