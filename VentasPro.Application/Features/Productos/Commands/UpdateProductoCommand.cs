namespace VentasPro.Application.Features.Productos.Commands;

public class UpdateProductoCommand
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public int Stock { get; set; }
    public string? CodigoBarras { get; set; }
    public int? CategoriaId { get; set; }
}