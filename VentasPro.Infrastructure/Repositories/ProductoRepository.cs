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

    public async Task<bool> CodigoBarrasExistsAsync(string codigoBarras, int? excludeId = null)
    {
        var query = _dbSet.Where(p => p.CodigoBarras == codigoBarras && p.Activo);
        if (excludeId.HasValue)
        {
            query = query.Where(p => p.Id != excludeId.Value);
        }
        return await query.AnyAsync();
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

    public override async Task<IEnumerable<Producto>> GetAllAsync(bool includeInactive = false)
    {
        var query = _dbSet.Include(p => p.Categoria).AsQueryable();
        if (!includeInactive)
        {
            query = query.Where(p => p.Activo);
        }
        return await query.ToListAsync();
    }
}