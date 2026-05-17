using VentasPro.Application.Features.Categorias.Commands;
using VentasPro.Application.Features.Categorias.DTOs;
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

    public async Task<IEnumerable<CategoriaDto>> GetAllAsync(bool? soloActivos = null)
    {
        var categorias = await _categoriaRepository.GetAllAsync(includeInactive: true);

        if (soloActivos.HasValue)
        {
            categorias = categorias.Where(c => c.Activo == soloActivos.Value);
        }

        return categorias.Select(MapToDto);
    }

    public async Task<CategoriaDto?> GetByIdAsync(int id)
    {
        var categoria = await _categoriaRepository.GetByIdAsync(id);
        return categoria == null ? null : MapToDto(categoria);
    }

    public async Task<CategoriaDto> CreateAsync(CreateCategoriaCommand command)
    {
        var categoria = new Categoria
        {
            Nombre = command.Nombre,
            Descripcion = command.Descripcion
        };

        await _categoriaRepository.AddAsync(categoria);
        return MapToDto(categoria);
    }

    public async Task<CategoriaDto> UpdateAsync(UpdateCategoriaCommand command)
    {
        var categoria = await _categoriaRepository.GetByIdAsync(command.Id)
            ?? throw new InvalidOperationException($"Categoría con ID {command.Id} no encontrada.");

        categoria.Nombre = command.Nombre;
        categoria.Descripcion = command.Descripcion;

        await _categoriaRepository.UpdateAsync(categoria);
        return MapToDto(categoria);
    }

    public async Task DeleteAsync(int id)
    {
        await _categoriaRepository.DeleteAsync(id);
    }

    public async Task ActivateAsync(int id)
    {
        var categoria = await _categoriaRepository.GetByIdAsync(id)
            ?? throw new InvalidOperationException($"Categoría con ID {id} no encontrada.");

        categoria.Activo = true;
        categoria.FechaModificacion = DateTime.UtcNow;
        await _categoriaRepository.UpdateAsync(categoria);
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