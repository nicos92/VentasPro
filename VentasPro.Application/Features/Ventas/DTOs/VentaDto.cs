namespace VentasPro.Application.Features.Ventas.DTOs;

public class DetalleVentaDto
{
    public int Id { get; set; }
    public int ProductoId { get; set; }
    public string ProductoNombre { get; set; } = string.Empty;
    public string? ProductoCodigo { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Subtotal { get; set; }
}

public class VentaDto
{
    public int Id { get; set; }
    public int? ClienteId { get; set; }
    public string ClienteNombre { get; set; } = "Consumidor Final";
    public DateTime FechaVenta { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Impuestos { get; set; }
    public decimal Total { get; set; }
    public string Estado { get; set; } = string.Empty;
    public string MetodoPago { get; set; } = string.Empty;
    public string? Notas { get; set; }
    public List<DetalleVentaDto> Detalles { get; set; } = new();
}