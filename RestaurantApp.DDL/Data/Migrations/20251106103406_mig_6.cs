using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RestaurantApp.DDL.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig_6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, null, "Başlanğıclar" },
                    { 2, null, "Əsas yeməklər" },
                    { 3, null, "Salatlar" },
                    { 4, null, "Desertlər" },
                    { 5, null, "İçkilər" }
                });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "Id", "Date", "TotalAmount" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 15, 12, 30, 0, 0, DateTimeKind.Unspecified), 35.50m },
                    { 2, new DateTime(2024, 1, 15, 13, 15, 0, 0, DateTimeKind.Unspecified), 52.00m },
                    { 3, new DateTime(2024, 1, 15, 14, 0, 0, 0, DateTimeKind.Unspecified), 28.50m },
                    { 4, new DateTime(2024, 1, 16, 11, 45, 0, 0, DateTimeKind.Unspecified), 67.00m },
                    { 5, new DateTime(2024, 1, 16, 12, 20, 0, 0, DateTimeKind.Unspecified), 41.50m }
                });

            migrationBuilder.InsertData(
                table: "MenuItem",
                columns: new[] { "Id", "CategoryId", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 1, "Çörək Səbəti", 3.50m },
                    { 2, 1, "Pomidor Şorbası", 5.00m },
                    { 3, 1, "Kükü", 4.50m },
                    { 4, 1, "Zeytun", 3.00m },
                    { 5, 1, "Pendir Seçimi", 6.50m },
                    { 6, 2, "Toyuq Şiş", 12.00m },
                    { 7, 2, "Lülə Kabab", 15.00m },
                    { 8, 2, "Balıq Filesi", 18.00m },
                    { 9, 2, "Plov", 10.00m },
                    { 10, 2, "Biftek", 20.00m },
                    { 11, 3, "Çoban Salatı", 5.50m },
                    { 12, 3, "Sezar Salatı", 7.00m },
                    { 13, 3, "Yunan Salatı", 6.50m },
                    { 14, 3, "Mangal Salatı", 6.00m },
                    { 15, 3, "Göyərti Salatı", 4.50m },
                    { 16, 4, "Tiramisu", 6.00m },
                    { 17, 4, "Şokolad Tortu", 5.50m },
                    { 18, 4, "Baklava", 4.00m },
                    { 19, 4, "Profiterol", 5.00m },
                    { 20, 4, "Cheesecake", 6.50m },
                    { 21, 5, "Kola", 2.00m },
                    { 22, 5, "Portağal Şirəsi", 3.50m },
                    { 23, 5, "Ayran", 2.50m },
                    { 24, 5, "Türk Qəhvəsi", 3.00m },
                    { 25, 5, "Çay", 1.50m }
                });

            migrationBuilder.InsertData(
                table: "OrderItem",
                columns: new[] { "Id", "Count", "MenuItemId", "OrderId" },
                values: new object[,]
                {
                    { 1, 2, 6, 1 },
                    { 2, 1, 2, 1 },
                    { 3, 2, 21, 1 },
                    { 4, 1, 7, 2 },
                    { 5, 3, 11, 2 },
                    { 6, 2, 23, 2 },
                    { 7, 2, 9, 3 },
                    { 8, 1, 15, 3 },
                    { 9, 2, 25, 3 },
                    { 10, 1, 8, 4 },
                    { 11, 2, 12, 4 },
                    { 12, 1, 18, 4 },
                    { 13, 3, 24, 4 },
                    { 14, 2, 10, 5 },
                    { 15, 1, 13, 5 },
                    { 16, 2, 16, 5 },
                    { 17, 1, 22, 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MenuItem",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MenuItem",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MenuItem",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MenuItem",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MenuItem",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "MenuItem",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "MenuItem",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "MenuItem",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "OrderItem",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderItem",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OrderItem",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OrderItem",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "OrderItem",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "OrderItem",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "OrderItem",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "OrderItem",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "OrderItem",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "OrderItem",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "OrderItem",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "OrderItem",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "OrderItem",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "OrderItem",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "OrderItem",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "OrderItem",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "OrderItem",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "MenuItem",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MenuItem",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "MenuItem",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "MenuItem",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "MenuItem",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "MenuItem",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "MenuItem",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "MenuItem",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "MenuItem",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "MenuItem",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "MenuItem",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "MenuItem",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "MenuItem",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "MenuItem",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "MenuItem",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "MenuItem",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "MenuItem",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
