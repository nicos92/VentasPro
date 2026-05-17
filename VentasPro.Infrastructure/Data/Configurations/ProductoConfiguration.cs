using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VentasPro.Domain.Entities;

namespace VentasPro.Infrastructure.Data.Configurations;

public class ProductoConfiguration : IEntityTypeConfiguration<Producto>
{
    public void Configure(EntityTypeBuilder<Producto> builder)
    {
        builder.ToTable("Productos");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Nombre).IsRequired().HasMaxLength(200);
        builder.Property(p => p.Descripcion).HasMaxLength(1000);
        builder.Property(p => p.PrecioCosto).HasColumnType("decimal(18,2)");
        builder.Property(p => p.PorcentajeGanancia).HasColumnType("decimal(18,2)");
        builder.Property(p => p.CodigoBarras).HasMaxLength(50);

        builder.Ignore(p => p.PrecioVenta);

        builder.HasIndex(p => p.CodigoBarras).IsUnique().HasFilter(null);

        builder.HasOne(p => p.Categoria)
            .WithMany(c => c.Productos)
            .HasForeignKey(p => p.CategoriaId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}