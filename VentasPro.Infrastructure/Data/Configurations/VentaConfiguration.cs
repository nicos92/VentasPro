using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VentasPro.Domain.Entities;

namespace VentasPro.Infrastructure.Data.Configurations;

public class VentaConfiguration : IEntityTypeConfiguration<Venta>
{
    public void Configure(EntityTypeBuilder<Venta> builder)
    {
        builder.ToTable("Ventas");
        builder.HasKey(v => v.Id);
        builder.Property(v => v.Subtotal).HasColumnType("decimal(18,2)");
        builder.Property(v => v.Impuestos).HasColumnType("decimal(18,2)");
        builder.Property(v => v.Total).HasColumnType("decimal(18,2)");
        builder.Property(v => v.Notas).HasMaxLength(1000);

        builder.HasOne(v => v.Cliente)
            .WithMany(c => c.Ventas)
            .HasForeignKey(v => v.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}