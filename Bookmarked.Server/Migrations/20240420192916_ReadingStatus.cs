using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bookmarked.Server.Migrations
{
    /// <inheritdoc />
    public partial class ReadingStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0fe135cf-3b22-499e-bd7f-ae859dd5d59c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d4a0eb53-4721-4357-b39a-b5c1cbdd88ee");

            migrationBuilder.AddColumn<int>(
                name: "ReadingStatus",
                table: "Bookshelves",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "596eafbe-5a4b-42be-a9d2-e08eafcc48d3", null, "Admin", "ADMIN" },
                    { "97700e57-2494-4932-8f59-0eca215ddffd", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "596eafbe-5a4b-42be-a9d2-e08eafcc48d3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "97700e57-2494-4932-8f59-0eca215ddffd");

            migrationBuilder.DropColumn(
                name: "ReadingStatus",
                table: "Bookshelves");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0fe135cf-3b22-499e-bd7f-ae859dd5d59c", null, "User", "USER" },
                    { "d4a0eb53-4721-4357-b39a-b5c1cbdd88ee", null, "Admin", "ADMIN" }
                });
        }
    }
}
