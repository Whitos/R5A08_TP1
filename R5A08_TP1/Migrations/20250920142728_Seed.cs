using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace R5A08_TP1.Migrations
{
    /// <inheritdoc />
    public partial class Seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "marque",
                columns: new[] { "id_marque", "nom_marque" },
                values: new object[,]
                {
                    { 1, "Apple" },
                    { 2, "Samsung" }
                });

            migrationBuilder.InsertData(
                table: "type_produit",
                columns: new[] { "id_type_produit", "nom_type_produit" },
                values: new object[,]
                {
                    { 1, "Smartphone" },
                    { 2, "Ordinateur" }
                });

            migrationBuilder.InsertData(
                table: "produit",
                columns: new[] { "id_produit", "description", "id_marque", "id_type_produit", "nom_photo", "nom_produit", "stock_max", "stock_min", "stock_reel", "uri_photo" },
                values: new object[,]
                {
                    { 1, "Dernier modèle Apple", 1, 1, "iphone15.jpg", "iPhone 15", 0, 0, 0, "/img/iphone15.jpg" },
                    { 2, "Dernier modèle Samsung", 2, 1, "galaxyS24.jpg", "Galaxy S24", 0, 0, 0, "/img/galaxyS24.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "produit",
                keyColumn: "id_produit",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "produit",
                keyColumn: "id_produit",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "type_produit",
                keyColumn: "id_type_produit",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "marque",
                keyColumn: "id_marque",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "marque",
                keyColumn: "id_marque",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "type_produit",
                keyColumn: "id_type_produit",
                keyValue: 1);
        }
    }
}
