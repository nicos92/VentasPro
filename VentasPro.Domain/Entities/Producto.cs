namespace VentasPro.Domain.Entities;

public class Producto : BaseEntity
{
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public decimal PrecioCosto { get; set; }
    public decimal PorcentajeGanancia { get; set; }
    public int Stock { get; set; }
    public string? CodigoBarras { get; set; }
    public int? CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }
    public int? ProveedorId { get; set; }
    public Proveedor? Proveedor { get; set; }
    public ICollection<DetalleVenta> DetallesVenta { get; set; } = new List<DetalleVenta>();

    public decimal PrecioVenta => PrecioCosto + (PrecioCosto * PorcentajeGanancia / 100);
}