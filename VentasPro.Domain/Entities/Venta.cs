using VentasPro.Domain.Enums;

namespace VentasPro.Domain.Entities;

public class Venta : BaseEntity
{
    public int ClienteId { get; set; }
    public Cliente? Cliente { get; set; }
    public DateTime FechaVenta { get; set; } = DateTime.UtcNow;
    public decimal Subtotal { get; set; }
    public decimal Impuestos { get; set; }
    public decimal Total { get; set; }
    public EstadoVenta Estado { get; set; } = EstadoVenta.Completada;
    public MetodoPago MetodoPago { get; set; } = MetodoPago.Efectivo;
    public string? Notas { get; set; }
    public ICollection<DetalleVenta> Detalles { get; set; } = new List<DetalleVenta>();
}