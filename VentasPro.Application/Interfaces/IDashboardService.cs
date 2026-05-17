using VentasPro.Application.Features.Dashboard.DTOs;

namespace VentasPro.Application.Interfaces;

public interface IDashboardService
{
    Task<DashboardDto> GetEstadisticasAsync();
}