using VentasPro.Application.Features.Categorias.Commands;
using VentasPro.Application.Features.Categorias.DTOs;

namespace VentasPro.Application.Interfaces;

public interface ICategoriaService
{
    Task<IEnumerable<CategoriaDto>> GetAllAsync(bool? soloActivos = null);
    Task<CategoriaDto?> GetByIdAsync(int id);
    Task<CategoriaDto> CreateAsync(CreateCategoriaCommand command);
    Task<CategoriaDto> UpdateAsync(UpdateCategoriaCommand command);
    Task DeleteAsync(int id);
    Task ActivateAsync(int id);
}