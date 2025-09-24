using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class FixProductoProveedorForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdProducto",
                table: "ProductosProveedores",
                newName: "ProductoId");

            migrationBuilder.RenameColumn(
                name: "IdProveedor",
                table: "ProductosProveedores",
                newName: "ProveedorId");
        }
    }
}
