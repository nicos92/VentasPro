namespace VentasPro.Application.Features.Proveedores.DTOs;

public class ProveedorDto
{
    public int Id { get; set; }
    public string CUIT { get; set; } = string.Empty;
    public string NombreEmpresa { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string? Tel { get; set; }
    public string? Email { get; set; }
    public string? Observacion { get; set; }
    public bool Activo { get; set; }
}