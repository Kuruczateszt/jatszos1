using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace wshop3.Migrations2
{
    /// <inheritdoc />
    public partial class Role1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "735a68cc-82fa-4a26-910a-e72de1ecfd19");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e891ac75-d2ea-4be0-a29d-3ff6aca30110");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "81b0e0a2-6503-414a-a183-6cae5df4f272", null, "ADMIN", "USER" },
                    { "f00df348-d456-4bed-95e5-19a80f09492b", null, "ADMIN", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "81b0e0a2-6503-414a-a183-6cae5df4f272");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f00df348-d456-4bed-95e5-19a80f09492b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "735a68cc-82fa-4a26-910a-e72de1ecfd19", null, "Admin", "ADMIN" },
                    { "e891ac75-d2ea-4be0-a29d-3ff6aca30110", null, "User", "USER" }
                });
        }
    }
}
