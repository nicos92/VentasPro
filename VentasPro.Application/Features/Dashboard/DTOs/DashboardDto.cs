namespace VentasPro.Application.Features.Dashboard.DTOs;

public class DashboardDto
{
    public int TotalProductos { get; set; }
    public int ProductosActivos { get; set; }
    public int ProductosInactivos { get; set; }
    public int CantidadProductosStockBajo { get; set; }
    public decimal ValorTotalInventario { get; set; }

    public int TotalClientes { get; set; }
    public int ClientesActivos { get; set; }
    public int ClientesInactivos { get; set; }

    public int TotalCategorias { get; set; }
    public int CategoriasActivas { get; set; }
    public int CategoriasInactivas { get; set; }

    public int TotalVentas { get; set; }
    public int VentasCompletadas { get; set; }
    public int VentasCanceladas { get; set; }
    public decimal VentasTotalesMonto { get; set; }
    public decimal VentasPromedio { get; set; }

    public List<VentaRecienteDto> VentasRecientes { get; set; } = new();
    public List<ProductoStockBajoDto> ListaStockBajo { get; set; } = new();
    public VentasPorMesDto VentasPorMes { get; set; } = new();
}

public class VentaRecienteDto
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public string Cliente { get; set; } = string.Empty;
    public decimal Total { get; set; }
    public string Estado { get; set; } = string.Empty;
}

public class ProductoStockBajoDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? CodigoBarras { get; set; }
    public int Stock { get; set; }
    public decimal Precio { get; set; }
}

public class VentasPorMesDto
{
    public List<string> Labels { get; set; } = new();
    public List<decimal> Valores { get; set; } = new();
}