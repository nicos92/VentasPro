using VentasPro.Domain.Entities;

namespace VentasPro.Domain.Repositories;

public interface IDetalleVentaRepository : IRepository<DetalleVenta>
{
    Task<IEnumerable<DetalleVenta>> GetByVentaAsync(int ventaId);
}