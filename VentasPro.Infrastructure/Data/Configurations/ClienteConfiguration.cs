using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VentasPro.Domain.Entities;

namespace VentasPro.Infrastructure.Data.Configurations;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("Clientes");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Nombre).IsRequired().HasMaxLength(100);
        builder.Property(c => c.Apellido).IsRequired().HasMaxLength(100);
        builder.Property(c => c.Email).HasMaxLength(200);
        builder.Property(c => c.Telefono).HasMaxLength(20);
        builder.Property(c => c.Direccion).HasMaxLength(500);
        builder.Property(c => c.Identificacion).HasMaxLength(50);
        builder.HasIndex(c => c.Identificacion).IsUnique();
    }
}