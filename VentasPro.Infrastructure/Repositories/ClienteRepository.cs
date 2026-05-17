using Microsoft.EntityFrameworkCore;
using VentasPro.Domain.Entities;
using VentasPro.Domain.Repositories;
using VentasPro.Infrastructure.Data.DbContext;

namespace VentasPro.Infrastructure.Repositories;

public class ClienteRepository : Repository<Cliente>, IClienteRepository
{
    public ClienteRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Cliente?> GetByIdentificacionAsync(string identificacion)
    {
        return await _dbSet.FirstOrDefaultAsync(c => c.Identificacion == identificacion && c.Activo);
    }

    public async Task<IEnumerable<Cliente>> SearchAsync(string searchTerm)
    {
        var term = searchTerm.ToLower();
        return await _dbSet
            .Where(c => c.Activo && (
                c.Nombre.ToLower().Contains(term) ||
                c.Apellido.ToLower().Contains(term) ||
                (c.Email != null && c.Email.ToLower().Contains(term)) ||
                (c.Identificacion != null && c.Identificacion.Contains(term))
            ))
            .ToListAsync();
    }
}