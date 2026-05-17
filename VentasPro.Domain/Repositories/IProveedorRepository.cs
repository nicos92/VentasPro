using VentasPro.Domain.Entities;

namespace VentasPro.Domain.Repositories;

public interface IProveedorRepository : IRepository<Proveedor>
{
    Task<Proveedor?> GetByCUITAsync(string cuit);
    Task<IEnumerable<Proveedor>> SearchAsync(string searchTerm);
}