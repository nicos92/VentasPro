using System.ComponentModel.DataAnnotations;

namespace VentasPro.Application.Features.Productos.Commands;

public class UpdateProductoCommand
{
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "El campo Nombre es requerido.")]
    [StringLength(200, ErrorMessage = "El campo Nombre debe tener como máximo 200 caracteres.")]
    public string Nombre { get; set; } = string.Empty;

    [StringLength(1000, ErrorMessage = "El campo Descripción debe tener como máximo 1000 caracteres.")]
    public string Descripcion { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo Precio es requerido.")]
    [Range(0, double.MaxValue, ErrorMessage = "El campo Precio debe ser un valor positivo.")]
    public decimal Precio { get; set; }

    [Required(ErrorMessage = "El campo Stock es requerido.")]
    [Range(0, int.MaxValue, ErrorMessage = "El campo Stock debe ser un valor positivo.")]
    public int Stock { get; set; }

    [StringLength(50, ErrorMessage = "El campo Código de Barras debe tener como máximo 50 caracteres.")]
    public string? CodigoBarras { get; set; }

    public int? CategoriaId { get; set; }
}