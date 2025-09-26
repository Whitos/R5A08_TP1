using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace R5A08_TP1.Migrations
{
    /// <inheritdoc />
    public partial class migrationproduitsdb2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "produit");

            migrationBuilder.DropTable(
                name: "marque");

            migrationBuilder.DropTable(
                name: "type_produit");

            migrationBuilder.CreateTable(
                name: "brand",
                columns: table => new
                {
                    id_brand = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name_brand = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_brand", x => x.id_brand);
                });

            migrationBuilder.CreateTable(
                name: "type_product",
                columns: table => new
                {
                    id_type_product = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name_type_product = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_type_product", x => x.id_type_product);
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    id_product = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name_product = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    name_photo = table.Column<string>(type: "text", nullable: false),
                    uri_photo = table.Column<string>(type: "text", nullable: true),
                    id_type_product = table.Column<int>(type: "integer", nullable: true),
                    id_brand = table.Column<int>(type: "integer", nullable: true),
                    actual_stock = table.Column<int>(type: "integer", nullable: false),
                    min_stock = table.Column<int>(type: "integer", nullable: false),
                    max_stock = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.id_product);
                    table.ForeignKey(
                        name: "FK_produits_marque",
                        column: x => x.id_brand,
                        principalTable: "brand",
                        principalColumn: "id_brand");
                    table.ForeignKey(
                        name: "FK_type_produit_produits",
                        column: x => x.id_type_product,
                        principalTable: "type_product",
                        principalColumn: "id_type_product");
                });

            migrationBuilder.InsertData(
                table: "brand",
                columns: new[] { "id_brand", "name_brand" },
                values: new object[,]
                {
                    { 1, "Apple" },
                    { 2, "Samsung" }
                });

            migrationBuilder.InsertData(
                table: "type_product",
                columns: new[] { "id_type_product", "name_type_product" },
                values: new object[,]
                {
                    { 1, "Smartphone" },
                    { 2, "Ordinateur" }
                });

            migrationBuilder.InsertData(
                table: "product",
                columns: new[] { "id_product", "actual_stock", "description", "id_brand", "id_type_product", "max_stock", "min_stock", "name_photo", "name_product", "uri_photo" },
                values: new object[,]
                {
                    { 1, 0, "Dernier modèle Apple", 1, 1, 0, 0, "iphone15.jpg", "iPhone 15", "/img/iphone15.jpg" },
                    { 2, 0, "Dernier modèle Samsung", 2, 1, 0, 0, "galaxyS24.jpg", "Galaxy S24", "/img/galaxyS24.jpg" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_product_id_brand",
                table: "product",
                column: "id_brand");

            migrationBuilder.CreateIndex(
                name: "IX_product_id_type_product",
                table: "product",
                column: "id_type_product");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "brand");

            migrationBuilder.DropTable(
                name: "type_product");

            migrationBuilder.CreateTable(
                name: "marque",
                columns: table => new
                {
                    id_marque = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nom_marque = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_marque", x => x.id_marque);
                });

            migrationBuilder.CreateTable(
                name: "type_produit",
                columns: table => new
                {
                    id_type_produit = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nom_type_produit = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_type_produit", x => x.id_type_produit);
                });

            migrationBuilder.CreateTable(
                name: "produit",
                columns: table => new
                {
                    id_produit = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_marque = table.Column<int>(type: "integer", nullable: true),
                    id_type_produit = table.Column<int>(type: "integer", nullable: true),
                    description = table.Column<string>(type: "text", nullable: false),
                    nom_photo = table.Column<string>(type: "text", nullable: false),
                    nom_produit = table.Column<string>(type: "text", nullable: false),
                    stock_max = table.Column<int>(type: "integer", nullable: false),
                    stock_min = table.Column<int>(type: "integer", nullable: false),
                    stock_reel = table.Column<int>(type: "integer", nullable: false),
                    uri_photo = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_produit", x => x.id_produit);
                    table.ForeignKey(
                        name: "FK_produits_marque",
                        column: x => x.id_marque,
                        principalTable: "marque",
                        principalColumn: "id_marque");
                    table.ForeignKey(
                        name: "FK_type_produit_produits",
                        column: x => x.id_type_produit,
                        principalTable: "type_produit",
                        principalColumn: "id_type_produit");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_produit_id_marque",
                table: "produit",
                column: "id_marque");

            migrationBuilder.CreateIndex(
                name: "IX_produit_id_type_produit",
                table: "produit",
                column: "id_type_produit");
        }
    }
}
