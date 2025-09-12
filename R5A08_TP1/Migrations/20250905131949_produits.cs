using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace R5A08_TP1.Migrations
{
    /// <inheritdoc />
    public partial class produits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_produits_type_produits",
                table: "Produit");

            migrationBuilder.AddForeignKey(
                name: "FK_type_produit_produits",
                table: "Produit",
                column: "id_type_produit",
                principalTable: "TypeProduit",
                principalColumn: "id_type_produit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_type_produit_produits",
                table: "Produit");

            migrationBuilder.AddForeignKey(
                name: "FK_produits_type_produits",
                table: "Produit",
                column: "id_type_produit",
                principalTable: "TypeProduit",
                principalColumn: "id_type_produit");
        }
    }
}
