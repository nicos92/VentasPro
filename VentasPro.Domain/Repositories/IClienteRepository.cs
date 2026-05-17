using VentasPro.Domain.Entities;

namespace VentasPro.Domain.Repositories;

public interface IClienteRepository : IRepository<Cliente>
{
    Task<Cliente?> GetByIdentificacionAsync(string identificacion);
    Task<IEnumerable<Cliente>> SearchAsync(string searchTerm);
}