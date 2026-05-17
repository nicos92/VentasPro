using VentasPro.Application.Features.Productos.Commands;
using VentasPro.Application.Features.Productos.DTOs;
using VentasPro.Application.Features.Productos.Queries;
using VentasPro.Application.Interfaces;
using VentasPro.Domain.Entities;
using VentasPro.Domain.Repositories;

namespace VentasPro.Application.Services;

public class ProductoService : IProductoService
{
    private readonly IProductoRepository _productoRepository;

    public ProductoService(IProductoRepository productoRepository)
    {
        _productoRepository = productoRepository;
    }

    public async Task<IEnumerable<ProductoDto>> GetAllAsync(bool? soloActivos = null)
    {
        IEnumerable<Producto> productos = await _productoRepository.GetAllAsync(includeInactive: true);

        if (soloActivos.HasValue)
        {
            productos = productos.Where(p => p.Activo == soloActivos.Value);
        }

        return productos.Select(MapToDto);
    }

    public async Task<ProductoDto?> GetByIdAsync(int id)
    {
        var producto = await _productoRepository.GetByIdAsync(id);
        return producto == null ? null : MapToDto(producto);
    }

    public async Task<ProductoDto> CreateAsync(CreateProductoCommand command)
    {
        if (!string.IsNullOrWhiteSpace(command.CodigoBarras))
        {
            var existe = await _productoRepository.CodigoBarrasExistsAsync(command.CodigoBarras);
            if (existe)
            {
                throw new InvalidOperationException($"El código de barras '{command.CodigoBarras}' ya está registrado.");
            }
        }

        var producto = new Producto
        {
            Nombre = command.Nombre,
            Descripcion = command.Descripcion,
            Precio = command.Precio,
            Stock = command.Stock,
            CodigoBarras = command.CodigoBarras,
            CategoriaId = command.CategoriaId
        };

        await _productoRepository.AddAsync(producto);
        return MapToDto(producto);
    }

    public async Task<ProductoDto> UpdateAsync(UpdateProductoCommand command)
    {
        var producto = await _productoRepository.GetByIdAsync(command.Id)
            ?? throw new InvalidOperationException($"Producto con ID {command.Id} no encontrado.");

        if (!string.IsNullOrWhiteSpace(command.CodigoBarras))
        {
            var existe = await _productoRepository.CodigoBarrasExistsAsync(command.CodigoBarras, command.Id);
            if (existe)
            {
                throw new InvalidOperationException($"El código de barras '{command.CodigoBarras}' ya está registrado.");
            }
        }

        producto.Nombre = command.Nombre;
        producto.Descripcion = command.Descripcion;
        producto.Precio = command.Precio;
        producto.Stock = command.Stock;
        producto.CodigoBarras = command.CodigoBarras;
        producto.CategoriaId = command.CategoriaId;

        await _productoRepository.UpdateAsync(producto);
        return MapToDto(producto);
    }

    public async Task DeleteAsync(int id)
    {
        await _productoRepository.DeleteAsync(id);
    }

    public async Task ActivateAsync(int id)
    {
        var producto = await _productoRepository.GetByIdAsync(id)
            ?? throw new InvalidOperationException($"Producto con ID {id} no encontrado.");

        producto.Activo = true;
        producto.FechaModificacion = DateTime.UtcNow;
        await _productoRepository.UpdateAsync(producto);
    }

    public async Task<IEnumerable<ProductoDto>> GetByCategoriaAsync(int categoriaId)
    {
        var productos = await _productoRepository.GetByCategoriaAsync(categoriaId);
        return productos.Select(MapToDto);
    }

    public async Task<IEnumerable<ProductoDto>> GetLowStockAsync(int umbral = 10)
    {
        var productos = await _productoRepository.GetLowStockAsync(umbral);
        return productos.Select(MapToDto);
    }

    private static ProductoDto MapToDto(Producto producto)
    {
        return new ProductoDto
        {
            Id = producto.Id,
            Nombre = producto.Nombre,
            Descripcion = producto.Descripcion,
            Precio = producto.Precio,
            Stock = producto.Stock,
            CodigoBarras = producto.CodigoBarras,
            CategoriaId = producto.CategoriaId,
            CategoriaNombre = producto.Categoria?.Nombre,
            Activo = producto.Activo
        };
    }
}