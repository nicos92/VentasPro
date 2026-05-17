using Microsoft.EntityFrameworkCore;
using VentasPro.Domain.Entities;
using VentasPro.Domain.Enums;
using VentasPro.Domain.Repositories;
using VentasPro.Infrastructure.Data.DbContext;

namespace VentasPro.Infrastructure.Repositories;

public class VentaRepository : Repository<Venta>, IVentaRepository
{
    public VentaRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Venta?> GetWithDetailsAsync(int id)
    {
        return await _dbSet
            .Where(v => v.Id == id && v.Activo)
            .Include(v => v.Cliente)
            .Include(v => v.Detalles)
                .ThenInclude(d => d.Producto)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Venta>> GetByClienteAsync(int clienteId)
    {
        return await _dbSet
            .Where(v => v.ClienteId == clienteId && v.Activo)
            .Include(v => v.Cliente)
            .OrderByDescending(v => v.FechaVenta)
            .ToListAsync();
    }

    public async Task<IEnumerable<Venta>> GetByDateRangeAsync(DateTime fechaInicio, DateTime fechaFin)
    {
        return await _dbSet
            .Where(v => v.FechaVenta >= fechaInicio && v.FechaVenta <= fechaFin && v.Activo)
            .Include(v => v.Cliente)
            .OrderByDescending(v => v.FechaVenta)
            .ToListAsync();
    }

    public async Task<IEnumerable<Venta>> GetByEstadoAsync(EstadoVenta estado)
    {
        return await _dbSet
            .Where(v => v.Estado == estado && v.Activo)
            .Include(v => v.Cliente)
            .OrderByDescending(v => v.FechaVenta)
            .ToListAsync();
    }
}