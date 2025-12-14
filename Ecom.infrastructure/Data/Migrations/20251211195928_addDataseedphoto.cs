using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecom.infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class addDataseedphoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Photos",
                columns: new[] { "Id", "ImagName", "ProductId" },
                values: new object[,]
                {
                    { 1, "photo1.jpg", 1 },
                    { 2, "photo2.jpg", 1 },
                    { 3, "photo3.jpg", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
