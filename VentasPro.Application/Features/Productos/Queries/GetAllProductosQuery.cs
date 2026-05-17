namespace VentasPro.Application.Features.Productos.Queries;

public class GetAllProductosQuery
{
    public int? CategoriaId { get; set; }
    public bool? SoloActivos { get; set; } = true;
}