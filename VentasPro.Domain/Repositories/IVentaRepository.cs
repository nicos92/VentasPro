using VentasPro.Domain.Entities;
using VentasPro.Domain.Enums;

namespace VentasPro.Domain.Repositories;

public interface IVentaRepository : IRepository<Venta>
{
    Task<Venta?> GetWithDetailsAsync(int id);
    Task<IEnumerable<Venta>> GetByClienteAsync(int clienteId);
    Task<IEnumerable<Venta>> GetByDateRangeAsync(DateTime fechaInicio, DateTime fechaFin);
    Task<IEnumerable<Venta>> GetByEstadoAsync(EstadoVenta estado);
}