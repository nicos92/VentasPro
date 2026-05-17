namespace VentasPro.Domain.Entities;

public class DetalleVenta : BaseEntity
{
    public int VentaId { get; set; }
    public Venta? Venta { get; set; }

    public int ProductoId { get; set; }
    public Producto? Producto { get; set; }

    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Subtotal { get; set; }
}