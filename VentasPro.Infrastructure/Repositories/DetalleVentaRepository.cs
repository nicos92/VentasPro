using Microsoft.EntityFrameworkCore;
using VentasPro.Domain.Entities;
using VentasPro.Domain.Repositories;
using VentasPro.Infrastructure.Data.DbContext;

namespace VentasPro.Infrastructure.Repositories;

public class DetalleVentaRepository : Repository<DetalleVenta>, IDetalleVentaRepository
{
    public DetalleVentaRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<DetalleVenta>> GetByVentaAsync(int ventaId)
    {
        return await _dbSet
            .Where(d => d.VentaId == ventaId && d.Activo)
            .Include(d => d.Producto)
            .ToListAsync();
    }

    public override async Task<IEnumerable<DetalleVenta>> GetAllAsync(bool includeInactive = false)
    {
        var query = _dbSet.Include(d => d.Producto).AsQueryable();
        if (!includeInactive)
        {
            query = query.Where(d => d.Activo);
        }
        return await query.ToListAsync();
    }
}