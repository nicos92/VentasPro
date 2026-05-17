using System.ComponentModel.DataAnnotations;

namespace VentasPro.Application.Features.Proveedores.Commands;

public class CreateProveedorCommand
{
    [Required(ErrorMessage = "El campo CUIT es requerido.")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "El CUIT debe tener exactamente 11 caracteres.")]
    public string CUIT { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Nombre de Empresa es requerido.")]
    [StringLength(255, ErrorMessage = "El campo Nombre de Empresa debe tener como máximo 255 caracteres.")]
    public string NombreEmpresa { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Nombre es requerido.")]
    [StringLength(255, ErrorMessage = "El campo Nombre debe tener como máximo 255 caracteres.")]
    public string Nombre { get; set; } = string.Empty;

    [StringLength(255, ErrorMessage = "El campo Teléfono debe tener como máximo 255 caracteres.")]
    public string? Tel { get; set; }

    [EmailAddress(ErrorMessage = "El campo Email debe ser una dirección de correo válida.")]
    [StringLength(255, ErrorMessage = "El campo Email debe tener como máximo 255 caracteres.")]
    public string? Email { get; set; }

    [StringLength(255, ErrorMessage = "El campo Observación debe tener como máximo 255 caracteres.")]
    public string? Observacion { get; set; }
}