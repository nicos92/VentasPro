using VentasPro.Application.Features.Clientes.Commands;
using VentasPro.Application.Features.Clientes.DTOs;

namespace VentasPro.Application.Interfaces;

public interface IClienteService
{
    Task<IEnumerable<ClienteDto>> GetAllAsync(bool? soloActivos = null);
    Task<ClienteDto?> GetByIdAsync(int id);
    Task<ClienteDto> CreateAsync(CreateClienteCommand command);
    Task<ClienteDto> UpdateAsync(UpdateClienteCommand command);
    Task DeleteAsync(int id);
    Task ActivateAsync(int id);
    Task<IEnumerable<ClienteDto>> SearchAsync(string searchTerm);
    Task<ClienteDto?> GetByIdentificacionAsync(string identificacion);
}