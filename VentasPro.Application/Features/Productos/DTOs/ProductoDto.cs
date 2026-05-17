namespace VentasPro.Application.Features.Productos.DTOs;

public class ProductoDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public decimal PrecioCosto { get; set; }
    public decimal PorcentajeGanancia { get; set; }
    public decimal PrecioVenta { get; set; }
    public int Stock { get; set; }
    public string? CodigoBarras { get; set; }
    public int? CategoriaId { get; set; }
    public string? CategoriaNombre { get; set; }
    public int? ProveedorId { get; set; }
    public string? ProveedorNombre { get; set; }
    public bool Activo { get; set; }
}