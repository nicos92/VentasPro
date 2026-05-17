using Microsoft.EntityFrameworkCore;
using VentasPro.Domain.Entities;
using VentasPro.Domain.Repositories;
using VentasPro.Infrastructure.Data.DbContext;

namespace VentasPro.Infrastructure.Repositories;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Categoria?> GetByNombreAsync(string nombre)
    {
        return await _dbSet.FirstOrDefaultAsync(c => c.Nombre == nombre && c.Activo);
    }
}