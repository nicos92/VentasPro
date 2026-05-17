using System.ComponentModel.DataAnnotations;

namespace VentasPro.Application.Features.Ventas.Commands;

public class CreateVentaCommand
{
    public int? ClienteId { get; set; }

    public string? Notas { get; set; }

    [Required(ErrorMessage = "Debe seleccionar el método de pago.")]
    public int MetodoPago { get; set; }

    [Required(ErrorMessage = "Debe agregar al menos un producto a la venta.")]
    [MinLength(1, ErrorMessage = "Debe agregar al menos un producto a la venta.")]
    public List<DetalleVentaItem> Detalles { get; set; } = new();

    public decimal PorcentajeImpuesto { get; set; } = 0m;
}

public class DetalleVentaItem
{
    [Required(ErrorMessage = "Debe seleccionar un producto.")]
    public int ProductoId { get; set; }

    [Required(ErrorMessage = "La cantidad es requerida.")]
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0.")]
    public int Cantidad { get; set; }
}