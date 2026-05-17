using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VentasPro.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenamePrecioToPrecioCostoAndAddPorcentajeGanancia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Precio",
                table: "Productos",
                newName: "PrecioCosto");

            migrationBuilder.AddColumn<decimal>(
                name: "PorcentajeGanancia",
                table: "Productos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PorcentajeGanancia",
                table: "Productos");

            migrationBuilder.RenameColumn(
                name: "PrecioCosto",
                table: "Productos",
                newName: "Precio");
        }
    }
}
