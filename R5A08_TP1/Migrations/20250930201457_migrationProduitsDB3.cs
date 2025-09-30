using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace R5A08_TP1.Migrations
{
    /// <inheritdoc />
    public partial class migrationProduitsDB3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "product",
                keyColumn: "id_product",
                keyValue: 1,
                columns: new[] { "actual_stock", "max_stock", "min_stock" },
                values: new object[] { 10, 50, 5 });

            migrationBuilder.UpdateData(
                table: "product",
                keyColumn: "id_product",
                keyValue: 2,
                columns: new[] { "actual_stock", "max_stock", "min_stock" },
                values: new object[] { 15, 50, 5 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "product",
                keyColumn: "id_product",
                keyValue: 1,
                columns: new[] { "actual_stock", "max_stock", "min_stock" },
                values: new object[] { 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "product",
                keyColumn: "id_product",
                keyValue: 2,
                columns: new[] { "actual_stock", "max_stock", "min_stock" },
                values: new object[] { 0, 0, 0 });
        }
    }
}
