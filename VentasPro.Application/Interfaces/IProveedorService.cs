using VentasPro.Application.Features.Proveedores.Commands;
using VentasPro.Application.Features.Proveedores.DTOs;

namespace VentasPro.Application.Interfaces;

public interface IProveedorService
{
    Task<IEnumerable<ProveedorDto>> GetAllAsync(bool? soloActivos = null);
    Task<ProveedorDto?> GetByIdAsync(int id);
    Task<ProveedorDto> CreateAsync(CreateProveedorCommand command);
    Task<ProveedorDto> UpdateAsync(UpdateProveedorCommand command);
    Task DeleteAsync(int id);
    Task ActivateAsync(int id);
    Task<IEnumerable<ProveedorDto>> SearchAsync(string searchTerm);
    Task<ProveedorDto?> GetByCUITAsync(string cuit);
}