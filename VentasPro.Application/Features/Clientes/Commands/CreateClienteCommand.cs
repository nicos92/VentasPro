using System.ComponentModel.DataAnnotations;

namespace VentasPro.Application.Features.Clientes.Commands;

public class CreateClienteCommand
{
    [Required(ErrorMessage = "El campo Nombre es requerido.")]
    [StringLength(100, ErrorMessage = "El campo Nombre debe tener como máximo 100 caracteres.")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Apellido es requerido.")]
    [StringLength(100, ErrorMessage = "El campo Apellido debe tener como máximo 100 caracteres.")]
    public string Apellido { get; set; } = string.Empty;

    [EmailAddress(ErrorMessage = "El campo Email debe ser una dirección de correo válida.")]
    [StringLength(200, ErrorMessage = "El campo Email debe tener como máximo 200 caracteres.")]
    public string? Email { get; set; }

    [StringLength(20, ErrorMessage = "El campo Teléfono debe tener como máximo 20 caracteres.")]
    public string? Telefono { get; set; }

    [StringLength(500, ErrorMessage = "El campo Dirección debe tener como máximo 500 caracteres.")]
    public string? Direccion { get; set; }

    [StringLength(50, ErrorMessage = "El campo Identificación debe tener como máximo 50 caracteres.")]
    public string? Identificacion { get; set; }
}