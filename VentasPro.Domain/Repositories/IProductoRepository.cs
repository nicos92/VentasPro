using VentasPro.Domain.Entities;

namespace VentasPro.Domain.Repositories;

public interface IProductoRepository : IRepository<Producto>
{
    Task<Producto?> GetByCodigoBarrasAsync(string codigoBarras);
    Task<bool> CodigoBarrasExistsAsync(string codigoBarras, int? excludeId = null);
    Task<IEnumerable<Producto>> GetByCategoriaAsync(int categoriaId);
    Task<IEnumerable<Producto>> GetLowStockAsync(int umbral);
}