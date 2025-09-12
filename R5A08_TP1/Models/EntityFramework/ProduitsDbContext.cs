using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using R5A08_TP1.Models.EntityFramework;

namespace R5A08_TP1.Models.EntityFramework;

public partial class ProduitsDbContext : DbContext
{
    public DbSet<Produit> Produits { get; set; }
    public ProduitsDbContext()
    {
    }

    public ProduitsDbContext(DbContextOptions<ProduitsDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;port=5432;Database=ProduitsDB; uid=postgres;password=postgres;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Produit>(e =>
        {
            e.HasKey(p => p.IdProduit);

            e.HasOne(p => p.MarqueAssoc)
                .WithMany(m => m.ProduitsAssocMarques)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_produits_marque");

            e.HasOne(p => p.TypeProduitAssoc)
                .WithMany(m => m.ProduitsAssoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_produits_type_produits");
        });

        modelBuilder.Entity<TypeProduit>(e =>
        {
            e.HasKey(tp => tp.IdTypeProduit);

            e.HasMany(tp => tp.ProduitsAssoc)
                .WithOne(p => p.TypeProduitAssoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_type_produit_produits");

        });
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

public DbSet<R5A08_TP1.Models.EntityFramework.Produit> Produit { get; set; } = default!;
}
