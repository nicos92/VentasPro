using VentasPro.Application.Features.Productos.Commands;
using VentasPro.Application.Features.Productos.DTOs;
using VentasPro.Application.Features.Productos.Queries;

namespace VentasPro.Application.Interfaces;

public interface IProductoService
{
    Task<IEnumerable<ProductoDto>> GetAllAsync(GetAllProductosQuery? query = null);
    Task<ProductoDto?> GetByIdAsync(int id);
    Task<ProductoDto> CreateAsync(CreateProductoCommand command);
    Task<ProductoDto> UpdateAsync(UpdateProductoCommand command);
    Task DeleteAsync(int id);
    Task<IEnumerable<ProductoDto>> GetByCategoriaAsync(int categoriaId);
    Task<IEnumerable<ProductoDto>> GetLowStockAsync(int umbral = 10);
}