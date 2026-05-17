using VentasPro.Application.Features.Ventas.Commands;
using VentasPro.Application.Features.Ventas.DTOs;
using VentasPro.Domain.Enums;

namespace VentasPro.Application.Interfaces;

public interface IVentaService
{
    Task<IEnumerable<VentaDto>> GetAllAsync(bool? soloActivos = null);
    Task<VentaDto?> GetByIdAsync(int id);
    Task<VentaDto> CreateAsync(CreateVentaCommand command);
    Task CancelarAsync(int id);
    Task<IEnumerable<VentaDto>> GetByClienteAsync(int clienteId);
    Task<IEnumerable<VentaDto>> GetByDateRangeAsync(DateTime fechaInicio, DateTime fechaFin);
    Task<IEnumerable<VentaDto>> GetByEstadoAsync(EstadoVenta estado);
}