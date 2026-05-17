namespace VentasPro.Domain.Entities;

public class Proveedor : BaseEntity
{
    public string CUIT { get; set; } = string.Empty;
    public string NombreEmpresa { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string? Tel { get; set; }
    public string? Email { get; set; }
    public string? Observacion { get; set; }
    public ICollection<Producto> Productos { get; set; } = new List<Producto>();
}