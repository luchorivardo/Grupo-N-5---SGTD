using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class FixTablasIntermedias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductosProveedores_Productos_ProductosId",
                table: "ProductosProveedores");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductosProveedores_Proveedores_ProveedoresId",
                table: "ProductosProveedores");

            migrationBuilder.RenameColumn(
                name: "ProveedoresId",
                table: "ProductosProveedores",
                newName: "ProveedorId");

            migrationBuilder.RenameColumn(
                name: "ProductosId",
                table: "ProductosProveedores",
                newName: "ProductoId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductosProveedores_ProveedoresId",
                table: "ProductosProveedores",
                newName: "IX_ProductosProveedores_ProveedorId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductosProveedores_ProductosId",
                table: "ProductosProveedores",
                newName: "IX_ProductosProveedores_ProductoId");

            migrationBuilder.AddColumn<int>(
                name: "IdProducto",
                table: "ProductosProveedores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdProveedor",
                table: "ProductosProveedores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductosProveedores_Productos_ProductoId",
                table: "ProductosProveedores",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductosProveedores_Proveedores_ProveedorId",
                table: "ProductosProveedores",
                column: "ProveedorId",
                principalTable: "Proveedores",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductosProveedores_Productos_ProductoId",
                table: "ProductosProveedores");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductosProveedores_Proveedores_ProveedorId",
                table: "ProductosProveedores");

            migrationBuilder.DropColumn(
                name: "IdProducto",
                table: "ProductosProveedores");

            migrationBuilder.DropColumn(
                name: "IdProveedor",
                table: "ProductosProveedores");

            migrationBuilder.RenameColumn(
                name: "ProveedorId",
                table: "ProductosProveedores",
                newName: "ProveedoresId");

            migrationBuilder.RenameColumn(
                name: "ProductoId",
                table: "ProductosProveedores",
                newName: "ProductosId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductosProveedores_ProveedorId",
                table: "ProductosProveedores",
                newName: "IX_ProductosProveedores_ProveedoresId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductosProveedores_ProductoId",
                table: "ProductosProveedores",
                newName: "IX_ProductosProveedores_ProductosId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductosProveedores_Productos_ProductosId",
                table: "ProductosProveedores",
                column: "ProductosId",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductosProveedores_Proveedores_ProveedoresId",
                table: "ProductosProveedores",
                column: "ProveedoresId",
                principalTable: "Proveedores",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
