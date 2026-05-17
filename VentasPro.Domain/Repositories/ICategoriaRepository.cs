using VentasPro.Domain.Entities;

namespace VentasPro.Domain.Repositories;

public interface ICategoriaRepository : IRepository<Categoria>
{
    Task<Categoria?> GetByNombreAsync(string nombre);
}