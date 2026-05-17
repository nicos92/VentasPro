using Microsoft.EntityFrameworkCore;
using VentasPro.Domain.Entities;
using VentasPro.Domain.Repositories;
using VentasPro.Infrastructure.Data.DbContext;

namespace VentasPro.Infrastructure.Repositories;

public class ProveedorRepository : Repository<Proveedor>, IProveedorRepository
{
    public ProveedorRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Proveedor?> GetByCUITAsync(string cuit)
    {
        return await _dbSet.FirstOrDefaultAsync(p => p.CUIT == cuit && p.Activo);
    }

    public async Task<IEnumerable<Proveedor>> SearchAsync(string searchTerm)
    {
        var term = searchTerm.ToLower();
        return await _dbSet
            .Where(p => p.Activo && (
                p.CUIT.ToLower().Contains(term) ||
                p.NombreEmpresa.ToLower().Contains(term) ||
                p.Nombre.ToLower().Contains(term) ||
                (p.Email != null && p.Email.ToLower().Contains(term))
            ))
            .ToListAsync();
    }
}