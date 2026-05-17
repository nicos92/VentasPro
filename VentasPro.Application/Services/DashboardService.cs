using VentasPro.Application.Features.Dashboard.DTOs;
using VentasPro.Application.Interfaces;
using VentasPro.Domain.Enums;
using VentasPro.Domain.Repositories;

namespace VentasPro.Application.Services;

public class DashboardService : IDashboardService
{
    private readonly IProductoRepository _productoRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly ICategoriaRepository _categoriaRepository;
    private readonly IVentaRepository _ventaRepository;

    public DashboardService(
        IProductoRepository productoRepository,
        IClienteRepository clienteRepository,
        ICategoriaRepository categoriaRepository,
        IVentaRepository ventaRepository)
    {
        _productoRepository = productoRepository;
        _clienteRepository = clienteRepository;
        _categoriaRepository = categoriaRepository;
        _ventaRepository = ventaRepository;
    }

    public async Task<DashboardDto> GetEstadisticasAsync()
    {
        var productos = (await _productoRepository.GetAllAsync(includeInactive: true)).ToList();
        var clientes = (await _clienteRepository.GetAllAsync(includeInactive: true)).ToList();
        var categorias = (await _categoriaRepository.GetAllAsync(includeInactive: true)).ToList();
        var ventas = (await _ventaRepository.GetAllAsync(includeInactive: true)).ToList();

        var productosActivos = productos.Where(p => p.Activo).ToList();
        var clientesActivos = clientes.Where(c => c.Activo).ToList();
        var categoriasActivas = categorias.Where(c => c.Activo).ToList();
        var ventasCompletadas = ventas.Where(v => v.Estado == EstadoVenta.Completada).ToList();

        var productosStockBajo = productosActivos.Where(p => p.Stock <= 10).ToList();

        var ventasRecientes = ventas
            .OrderByDescending(v => v.FechaVenta)
            .Take(10)
            .Select(v => new VentaRecienteDto
            {
                Id = v.Id,
                Fecha = v.FechaVenta,
                Cliente = v.Cliente?.NombreCompleto ?? "Consumidor Final",
                Total = v.Total,
                Estado = v.Estado.ToString()
            })
            .ToList();

        var ventasPorMes = CalcularVentasPorMes(ventasCompletadas);

        return new DashboardDto
        {
            TotalProductos = productos.Count,
            ProductosActivos = productosActivos.Count,
            ProductosInactivos = productos.Count - productosActivos.Count,
            CantidadProductosStockBajo = productosStockBajo.Count,
            ValorTotalInventario = productosActivos.Sum(p => p.Precio * p.Stock),

            TotalClientes = clientes.Count,
            ClientesActivos = clientesActivos.Count,
            ClientesInactivos = clientes.Count - clientesActivos.Count,

            TotalCategorias = categorias.Count,
            CategoriasActivas = categoriasActivas.Count,
            CategoriasInactivas = categorias.Count - categoriasActivas.Count,

            TotalVentas = ventas.Count,
            VentasCompletadas = ventasCompletadas.Count,
            VentasCanceladas = ventas.Count(v => v.Estado == EstadoVenta.Cancelada || v.Estado == EstadoVenta.Anulada),
            VentasTotalesMonto = ventasCompletadas.Sum(v => v.Total),
            VentasPromedio = ventasCompletadas.Any() ? ventasCompletadas.Average(v => v.Total) : 0,

            VentasRecientes = ventasRecientes,
            ListaStockBajo = productosStockBajo
                .OrderBy(p => p.Stock)
                .Take(10)
                .Select(p => new ProductoStockBajoDto
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    CodigoBarras = p.CodigoBarras,
                    Stock = p.Stock,
                    Precio = p.Precio
                })
                .ToList(),
            VentasPorMes = ventasPorMes
        };
    }

    private static VentasPorMesDto CalcularVentasPorMes(List<Domain.Entities.Venta> ventasCompletadas)
    {
        var ultimosSeisMeses = Enumerable.Range(0, 6)
            .Select(i => DateTime.Now.AddMonths(-i))
            .Select(d => new { Mes = d.Month, Anio = d.Year, Label = d.ToString("MMM yyyy") })
            .OrderBy(x => x.Anio)
            .ThenBy(x => x.Mes)
            .ToList();

        var resultado = new VentasPorMesDto();
        resultado.Labels = ultimosSeisMeses.Select(x => x.Label).ToList();

        foreach (var mes in ultimosSeisMeses)
        {
            var total = ventasCompletadas
                .Where(v => v.FechaVenta.Month == mes.Mes && v.FechaVenta.Year == mes.Anio)
                .Sum(v => v.Total);
            resultado.Valores.Add(total);
        }

        return resultado;
    }
}