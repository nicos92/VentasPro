using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VentasPro.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeCodigoBarrasUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Productos_CodigoBarras",
                table: "Productos",
                column: "CodigoBarras",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Productos_CodigoBarras",
                table: "Productos");
        }
    }
}
