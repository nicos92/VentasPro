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

    [Required(ErrorMessage = "El campo Precio Costo es requerido.")]
    [Range(0, double.MaxValue, ErrorMessage = "El campo Precio Costo debe ser un valor positivo.")]
    public decimal PrecioCosto { get; set; }

    [Required(ErrorMessage = "El campo Porcentaje de Ganancia es requerido.")]
    [Range(0, 1000, ErrorMessage = "El Porcentaje de Ganancia debe ser entre 0 y 1000.")]
    public decimal PorcentajeGanancia { get; set; }

    [Required(ErrorMessage = "El campo Stock es requerido.")]
    public int Stock { get; set; }

    [StringLength(50, ErrorMessage = "El campo Código de Barras debe tener como máximo 50 caracteres.")]
    public string? CodigoBarras { get; set; }

    public int? CategoriaId { get; set; }

    public int? ProveedorId { get; set; }
}