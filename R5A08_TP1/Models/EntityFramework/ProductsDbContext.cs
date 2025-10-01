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

        // ---- SEEDING ----
        //modelBuilder.Entity<Brand>().HasData(
        //    new Brand { IdBrand = 1, NameBrand = "Apple" },
        //    new Brand { IdBrand = 2, NameBrand = "Samsung" }
        //);

        //modelBuilder.Entity<TypeProduct>().HasData(
        //    new TypeProduct { IdTypeProduct = 1, NameTypeProduct = "Smartphone" },
        //    new TypeProduct { IdTypeProduct = 2, NameTypeProduct = "Ordinateur" }
        //);

        //modelBuilder.Entity<Product>().HasData(
        //    new Product
        //    {
        //        IdProduct = 1,
        //        NameProduct = "iPhone 15",
        //        Description = "Dernier modèle Apple",
        //        NamePhoto = "iphone15.jpg",
        //        UriPhoto = "/img/iphone15.jpg",
        //        IdTypeProduct = 1,
        //        IdBrand = 1,
        //        ActualStock = 10,
        //        MinStock = 5,
        //        MaxStock = 50
        //    },
        //    new Product
        //    {
        //        IdProduct = 2,
        //        NameProduct = "Galaxy S24",
        //        Description = "Dernier modèle Samsung",
        //        NamePhoto = "galaxyS24.jpg",
        //        UriPhoto = "/img/galaxyS24.jpg",
        //        IdTypeProduct = 1,
        //        IdBrand = 2,
        //        ActualStock = 15,
        //        MinStock = 5,
        //        MaxStock = 50
        //    }
        //);

        //OnModelCreatingPartial(modelBuilder);
    //}

    //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

public DbSet<R5A08_TP1.Models.EntityFramework.Product> Product { get; set; } = default!;
}
