using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bookmarked.Server.Migrations
{
    /// <inheritdoc />
    public partial class PagesReadBookshelf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "596eafbe-5a4b-42be-a9d2-e08eafcc48d3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "97700e57-2494-4932-8f59-0eca215ddffd");

            migrationBuilder.AddColumn<int>(
                name: "PagesRead",
                table: "Bookshelves",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1ada17e4-de91-4e72-b4e6-cd3fd250714d", null, "Admin", "ADMIN" },
                    { "d3262c00-3c1b-4863-bc8f-d9d21742dcdb", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1ada17e4-de91-4e72-b4e6-cd3fd250714d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3262c00-3c1b-4863-bc8f-d9d21742dcdb");

            migrationBuilder.DropColumn(
                name: "PagesRead",
                table: "Bookshelves");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "596eafbe-5a4b-42be-a9d2-e08eafcc48d3", null, "Admin", "ADMIN" },
                    { "97700e57-2494-4932-8f59-0eca215ddffd", null, "User", "USER" }
                });
        }
    }
}
