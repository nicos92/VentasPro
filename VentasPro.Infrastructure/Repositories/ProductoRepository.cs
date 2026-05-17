using Microsoft.EntityFrameworkCore;
using VentasPro.Domain.Entities;
using VentasPro.Domain.Repositories;
using VentasPro.Infrastructure.Data.DbContext;

namespace VentasPro.Infrastructure.Repositories;

public class ProductoRepository : Repository<Producto>, IProductoRepository
{
    public ProductoRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Producto?> GetByCodigoBarrasAsync(string codigoBarras)
    {
        return await _dbSet.FirstOrDefaultAsync(p => p.CodigoBarras == codigoBarras && p.Activo);
    }

    public async Task<IEnumerable<Producto>> GetByCategoriaAsync(int categoriaId)
    {
        return await _dbSet
            .Where(p => p.CategoriaId == categoriaId && p.Activo)
            .Include(p => p.Categoria)
            .ToListAsync();
    }

    public async Task<IEnumerable<Producto>> GetLowStockAsync(int umbral)
    {
        return await _dbSet
            .Where(p => p.Stock <= umbral && p.Activo)
            .ToListAsync();
    }

    public override async Task<IEnumerable<Producto>> GetAllAsync()
    {
        return await _dbSet
            .Where(p => p.Activo)
            .Include(p => p.Categoria)
            .ToListAsync();
    }
}