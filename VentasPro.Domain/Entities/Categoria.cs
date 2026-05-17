namespace VentasPro.Domain.Entities;

public class Categoria : BaseEntity
{
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public ICollection<Producto> Productos { get; set; } = new List<Producto>();
}