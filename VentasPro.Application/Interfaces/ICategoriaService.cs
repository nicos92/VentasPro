using VentasPro.Application.Features.Clientes.DTOs;

namespace VentasPro.Application.Interfaces;

public interface ICategoriaService
{
    Task<IEnumerable<CategoriaDto>> GetAllAsync();
    Task<CategoriaDto?> GetByIdAsync(int id);
}