namespace VentasPro.Domain.Entities;

public class Producto : BaseEntity
{
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public int Stock { get; set; }
    public string? CodigoBarras { get; set; }
    public int? CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }
    public ICollection<DetalleVenta> DetallesVenta { get; set; } = new List<DetalleVenta>();
}