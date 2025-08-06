using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace projemaksut.Migrations
{
    /// <inheritdoc />
    public partial class migrationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "mahalleler",
                columns: new[] { "Id", "Ilce", "Name" },
                values: new object[,]
                {
                    { 10, "Beşiktaş", "Konaklar" },
                    { 11, "Beşiktaş", "Kuruçeşme" },
                    { 12, "Beşiktaş", "Kültür" },
                    { 13, "Beşiktaş", "Levazım" },
                    { 14, "Beşiktaş", "Levent" },
                    { 15, "Beşiktaş", "Mecidiye" },
                    { 16, "Beşiktaş", "Muradiye" },
                    { 17, "Beşiktaş", "Nispetiye" },
                    { 18, "Beşiktaş", "Ortaköy" },
                    { 19, "Beşiktaş", "Sinanpaşa" },
                    { 20, "Beşiktaş", "Türkali" },
                    { 21, "Beşiktaş", "Ulus" },
                    { 22, "Beşiktaş", "Vişnezade" },
                    { 23, "Beşiktaş", "Yıldız" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "mahalleler",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "mahalleler",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "mahalleler",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "mahalleler",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "mahalleler",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "mahalleler",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "mahalleler",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "mahalleler",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "mahalleler",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "mahalleler",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "mahalleler",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "mahalleler",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "mahalleler",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "mahalleler",
                keyColumn: "Id",
                keyValue: 23);
        }
    }
}
