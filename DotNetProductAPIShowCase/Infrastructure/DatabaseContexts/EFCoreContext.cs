using System;
using DotNetProductAPIShowCase.Domains;
using Microsoft.EntityFrameworkCore;

namespace DotNetProductAPIShowCase.Infrastructure.DatabaseContexts;

public class EFCoreContext : DbContext
{
    // Table name
    public DbSet<Product> Products { get; set; }

    public EFCoreContext(DbContextOptions<EFCoreContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("shopping");

        // Separate this configuration if have multiple entity to better organization
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.Id)
                .HasName("id");

            entity.Property(p => p.Name)
                .HasColumnType("varchar(50)")
                .HasMaxLength(50)
                .IsRequired()
                .HasColumnName("name");

            entity.HasIndex(p => p.Name)
                .IsUnique();

            entity.Property(p => p.Price)
                .HasColumnType("decimal(10, 2)")
                .IsRequired()
                .HasColumnName("price");

            entity.Property(p => p.Description)
                .HasColumnType("varchar(500)")
                .HasMaxLength(500)
                .IsRequired(false)
                .HasColumnName("description");
        });
    }

}
