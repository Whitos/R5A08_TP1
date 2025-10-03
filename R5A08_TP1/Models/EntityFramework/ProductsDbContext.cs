using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using R5A08_TP1.Models.EntityFramework;

namespace R5A08_TP1.Models.EntityFramework;

public partial class ProductsDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<TypeProduct> TypeProducts { get; set; }
    public ProductsDbContext()
    {
    }

    public ProductsDbContext(DbContextOptions<ProductsDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Server=localhost;port=5432;Database=ProduitsDB; uid=postgres;password=postgres;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(e =>
        {
            e.HasKey(p => p.IdProduct);

            e.HasOne(p => p.RelatedBrand)
                .WithMany(m => m.RelatedProductsBrands)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_produits_marque");

            e.HasOne(p => p.RelatedTypeProduct)
                .WithMany(m => m.RelatedProducts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_produits_type_produits");
        });

        modelBuilder.Entity<TypeProduct>(e =>
        {
            e.HasKey(tp => tp.IdTypeProduct);

            e.HasMany(tp => tp.RelatedProducts)
                .WithOne(p => p.RelatedTypeProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_type_produit_produits");

        });
    }

public DbSet<R5A08_TP1.Models.EntityFramework.Product> Product { get; set; } = default!;
}
