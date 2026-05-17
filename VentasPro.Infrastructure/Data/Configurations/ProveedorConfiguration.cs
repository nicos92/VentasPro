using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VentasPro.Domain.Entities;

namespace VentasPro.Infrastructure.Data.Configurations;

public class ProveedorConfiguration : IEntityTypeConfiguration<Proveedor>
{
    public void Configure(EntityTypeBuilder<Proveedor> builder)
    {
        builder.ToTable("Proveedores");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.CUIT).IsRequired().HasMaxLength(11);
        builder.Property(p => p.NombreEmpresa).IsRequired().HasMaxLength(255);
        builder.Property(p => p.Nombre).IsRequired().HasMaxLength(255);
        builder.Property(p => p.Tel).HasMaxLength(255);
        builder.Property(p => p.Email).HasMaxLength(255);
        builder.Property(p => p.Observacion).HasMaxLength(255);
        builder.HasIndex(p => p.CUIT).IsUnique();
    }
}