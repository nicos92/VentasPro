using VentasPro.Domain.Entities;

namespace VentasPro.Domain.Repositories;

public interface IProductoRepository : IRepository<Producto>
{
    Task<Producto?> GetByCodigoBarrasAsync(string codigoBarras);
    Task<IEnumerable<Producto>> GetByCategoriaAsync(int categoriaId);
    Task<IEnumerable<Producto>> GetLowStockAsync(int umbral);
}