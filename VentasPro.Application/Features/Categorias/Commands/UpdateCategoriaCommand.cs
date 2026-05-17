using System.ComponentModel.DataAnnotations;

namespace VentasPro.Application.Features.Categorias.Commands;

public class UpdateCategoriaCommand
{
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "El campo Nombre es requerido.")]
    [StringLength(100, ErrorMessage = "El campo Nombre debe tener como máximo 100 caracteres.")]
    public string Nombre { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "El campo Descripción debe tener como máximo 500 caracteres.")]
    public string? Descripcion { get; set; }
}