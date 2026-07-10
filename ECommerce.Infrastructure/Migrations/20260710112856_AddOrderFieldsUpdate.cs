using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderFieldsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "OrderItems",
                newName: "TotalAmount");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "OrderItems",
                newName: "Price");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "OrderItems",
                newName: "UnitPrice");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "OrderItems",
                newName: "TotalPrice");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedAt", "ImageUrl", "IsActive", "IsDeleted", "Name", "Price", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 7, 10, 8, 9, 27, 738, DateTimeKind.Utc).AddTicks(6701), "images/iphone16.jpg", true, false, "Apple iPhone 16", 89999m, null },
                    { 2, new DateTime(2026, 7, 10, 8, 9, 27, 738, DateTimeKind.Utc).AddTicks(6704), "images/galaxy-s25.jpg", true, false, "Samsung Galaxy S25", 79999m, null },
                    { 3, new DateTime(2026, 7, 10, 8, 9, 27, 738, DateTimeKind.Utc).AddTicks(6705), "images/oneplus13.jpg", true, false, "OnePlus 13", 64999m, null }
                });
        }
    }
}
