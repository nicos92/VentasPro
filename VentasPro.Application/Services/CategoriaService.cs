using VentasPro.Application.Features.Clientes.DTOs;
using VentasPro.Application.Interfaces;
using VentasPro.Domain.Entities;
using VentasPro.Domain.Repositories;

namespace VentasPro.Application.Services;

public class CategoriaService : ICategoriaService
{
    private readonly ICategoriaRepository _categoriaRepository;

    public CategoriaService(ICategoriaRepository categoriaRepository)
    {
        _categoriaRepository = categoriaRepository;
    }

    public async Task<IEnumerable<CategoriaDto>> GetAllAsync()
    {
        var categorias = await _categoriaRepository.GetAllAsync();
        return categorias.Select(MapToDto);
    }

    public async Task<CategoriaDto?> GetByIdAsync(int id)
    {
        var categoria = await _categoriaRepository.GetByIdAsync(id);
        return categoria == null ? null : MapToDto(categoria);
    }

    private static CategoriaDto MapToDto(Categoria categoria)
    {
        return new CategoriaDto
        {
            Id = categoria.Id,
            Nombre = categoria.Nombre,
            Descripcion = categoria.Descripcion,
            Activo = categoria.Activo
        };
    }
}