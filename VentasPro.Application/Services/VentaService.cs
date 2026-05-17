using VentasPro.Application.Features.Ventas.Commands;
using VentasPro.Application.Features.Ventas.DTOs;
using VentasPro.Application.Interfaces;
using VentasPro.Domain.Entities;
using VentasPro.Domain.Enums;
using VentasPro.Domain.Repositories;

namespace VentasPro.Application.Services;

public class VentaService : IVentaService
{
    private readonly IVentaRepository _ventaRepository;
    private readonly IDetalleVentaRepository _detalleVentaRepository;
    private readonly IProductoRepository _productoRepository;
    private readonly IClienteRepository _clienteRepository;

    public VentaService(
        IVentaRepository ventaRepository,
        IDetalleVentaRepository detalleVentaRepository,
        IProductoRepository productoRepository,
        IClienteRepository clienteRepository)
    {
        _ventaRepository = ventaRepository;
        _detalleVentaRepository = detalleVentaRepository;
        _productoRepository = productoRepository;
        _clienteRepository = clienteRepository;
    }

    public async Task<IEnumerable<VentaDto>> GetAllAsync(bool? soloActivos = null)
    {
        var ventas = await _ventaRepository.GetAllAsync(includeInactive: true);

        if (soloActivos.HasValue)
        {
            ventas = ventas.Where(v => v.Activo == soloActivos.Value);
        }

        return ventas.Select(MapToDto).OrderByDescending(v => v.FechaVenta);
    }

    public async Task<VentaDto?> GetByIdAsync(int id)
    {
        var venta = await _ventaRepository.GetWithDetailsAsync(id);
        return venta == null ? null : MapToDto(venta);
    }

    public async Task<VentaDto> CreateAsync(CreateVentaCommand command)
    {
        if (command.ClienteId.HasValue)
        {
            var cliente = await _clienteRepository.GetByIdAsync(command.ClienteId.Value);
            if (cliente == null || !cliente.Activo)
            {
                throw new InvalidOperationException("El cliente seleccionado no existe o no está activo.");
            }
        }

        var productosIds = command.Detalles.Select(d => d.ProductoId).Distinct().ToList();
        var productos = (await _productoRepository.GetAllAsync(includeInactive: true))
            .Where(p => productosIds.Contains(p.Id))
            .ToDictionary(p => p.Id);

        foreach (var detalle in command.Detalles)
        {
            if (!productos.TryGetValue(detalle.ProductoId, out var producto))
            {
                throw new InvalidOperationException($"El producto con ID {detalle.ProductoId} no existe.");
            }

            if (!producto.Activo)
            {
                throw new InvalidOperationException($"El producto '{producto.Nombre}' no está activo.");
            }
        }

        var venta = new Venta
        {
            ClienteId = command.ClienteId,
            FechaVenta = DateTime.UtcNow,
            MetodoPago = (MetodoPago)command.MetodoPago,
            Notas = command.Notas,
            Estado = EstadoVenta.Completada
        };

        decimal subtotal = 0;
        foreach (var detalle in command.Detalles)
        {
            var producto = productos[detalle.ProductoId];
            var precioUnitario = producto.PrecioVenta;
            var subtotalDetalle = precioUnitario * detalle.Cantidad;
            subtotal += subtotalDetalle;

            producto.Stock -= detalle.Cantidad;
            await _productoRepository.UpdateAsync(producto);

            venta.Detalles.Add(new DetalleVenta
            {
                ProductoId = producto.Id,
                PrecioUnitario = precioUnitario,
                Cantidad = detalle.Cantidad,
                Subtotal = subtotalDetalle
            });
        }

        venta.Subtotal = subtotal;
        venta.Impuestos = subtotal * (command.PorcentajeImpuesto / 100);
        venta.Total = venta.Subtotal + venta.Impuestos;

        await _ventaRepository.AddAsync(venta);

        return MapToDto(venta);
    }

    public async Task CancelarAsync(int id)
    {
        var venta = await _ventaRepository.GetWithDetailsAsync(id);
        if (venta == null)
        {
            throw new InvalidOperationException($"Venta con ID {id} no encontrada.");
        }

        if (venta.Estado == EstadoVenta.Cancelada || venta.Estado == EstadoVenta.Anulada)
        {
            throw new InvalidOperationException("Esta venta ya ha sido cancelada o anulada.");
        }

        foreach (var detalle in venta.Detalles)
        {
            var producto = await _productoRepository.GetByIdAsync(detalle.ProductoId);
            if (producto != null)
            {
                producto.Stock += detalle.Cantidad;
                await _productoRepository.UpdateAsync(producto);
            }
        }

        venta.Estado = EstadoVenta.Cancelada;
        venta.FechaModificacion = DateTime.UtcNow;
        await _ventaRepository.UpdateAsync(venta);
    }

    public async Task<IEnumerable<VentaDto>> GetByClienteAsync(int clienteId)
    {
        var ventas = await _ventaRepository.GetByClienteAsync(clienteId);
        return ventas.Select(MapToDto);
    }

    public async Task<IEnumerable<VentaDto>> GetByDateRangeAsync(DateTime fechaInicio, DateTime fechaFin)
    {
        var ventas = await _ventaRepository.GetByDateRangeAsync(fechaInicio, fechaFin);
        return ventas.Select(MapToDto);
    }

    public async Task<IEnumerable<VentaDto>> GetByEstadoAsync(EstadoVenta estado)
    {
        var ventas = await _ventaRepository.GetByEstadoAsync(estado);
        return ventas.Select(MapToDto);
    }

    private static VentaDto MapToDto(Venta venta)
    {
        return new VentaDto
        {
            Id = venta.Id,
            ClienteId = venta.ClienteId,
            ClienteNombre = venta.Cliente?.NombreCompleto ?? "N/A",
            FechaVenta = venta.FechaVenta,
            Subtotal = venta.Subtotal,
            Impuestos = venta.Impuestos,
            Total = venta.Total,
            Estado = venta.Estado.ToString(),
            MetodoPago = venta.MetodoPago.ToString(),
            Notas = venta.Notas,
            Detalles = venta.Detalles?.Select(d => new DetalleVentaDto
            {
                Id = d.Id,
                ProductoId = d.ProductoId,
                ProductoNombre = d.Producto?.Nombre ?? "N/A",
                ProductoCodigo = d.Producto?.CodigoBarras,
                Cantidad = d.Cantidad,
                PrecioUnitario = d.PrecioUnitario,
                Subtotal = d.Subtotal
            }).ToList() ?? new List<DetalleVentaDto>()
        };
    }
}