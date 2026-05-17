namespace VentasPro.Domain.Entities;

public class Cliente : BaseEntity
{
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Telefono { get; set; }
    public string? Direccion { get; set; }
    public string? Identificacion { get; set; }
    public ICollection<Venta> Ventas { get; set; } = new List<Venta>();

    public string NombreCompleto => $"{Nombre} {Apellido}";
}