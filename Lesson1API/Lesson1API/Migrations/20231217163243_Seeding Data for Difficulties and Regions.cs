using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Lesson1API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDataforDifficultiesandRegions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("811ee708-fc79-401a-9d90-8ce97da3a807"), "Khó 3" },
                    { new Guid("b0f4bf58-a690-4fc7-b6b9-2bc1be378752"), "Khó 2" },
                    { new Guid("ca76a67c-58b1-406d-b2a0-ee0db42bac77"), "Khó 1" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("0758eb25-1c29-47f9-8219-62b9de165eb6"), "TL", "Thái Lan", "some2_image-url.jpg" },
                    { new Guid("1554d0e1-aa56-4373-859f-e06d7970e6be"), "TQ", "Trung Quốc", "some3_image-url.jpg" },
                    { new Guid("c34de05e-a1e5-43e8-8664-96ffafb527dc"), "VN", "Việt Nam", "some1_image-url.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("811ee708-fc79-401a-9d90-8ce97da3a807"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("b0f4bf58-a690-4fc7-b6b9-2bc1be378752"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("ca76a67c-58b1-406d-b2a0-ee0db42bac77"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("0758eb25-1c29-47f9-8219-62b9de165eb6"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("1554d0e1-aa56-4373-859f-e06d7970e6be"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("c34de05e-a1e5-43e8-8664-96ffafb527dc"));
        }
    }
}
