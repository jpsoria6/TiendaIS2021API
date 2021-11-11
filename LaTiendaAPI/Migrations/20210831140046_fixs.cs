using Microsoft.EntityFrameworkCore.Migrations;

namespace LaTienda.API.Migrations
{
    public partial class fixs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LineaDeVentas_Stock_StockId",
                table: "LineaDeVentas");

            migrationBuilder.DropForeignKey(
                name: "FK_Producto_Rubros_RubroId",
                table: "Producto");

            migrationBuilder.DropForeignKey(
                name: "FK_Stock_Colores_ColorId",
                table: "Stock");

            migrationBuilder.DropForeignKey(
                name: "FK_Stock_Marcas_MarcaId",
                table: "Stock");

            migrationBuilder.DropForeignKey(
                name: "FK_Stock_Producto_ProductoId",
                table: "Stock");

            migrationBuilder.DropForeignKey(
                name: "FK_Stock_Sucursal_SucursalId",
                table: "Stock");

            migrationBuilder.DropForeignKey(
                name: "FK_Stock_Talles_TalleId",
                table: "Stock");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stock",
                table: "Stock");

            migrationBuilder.DropIndex(
                name: "IX_Stock_MarcaId",
                table: "Stock");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Producto",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "MarcaId",
                table: "Stock");

            migrationBuilder.RenameTable(
                name: "Stock",
                newName: "Stocks");

            migrationBuilder.RenameTable(
                name: "Producto",
                newName: "Productos");

            migrationBuilder.RenameIndex(
                name: "IX_Stock_TalleId",
                table: "Stocks",
                newName: "IX_Stocks_TalleId");

            migrationBuilder.RenameIndex(
                name: "IX_Stock_SucursalId",
                table: "Stocks",
                newName: "IX_Stocks_SucursalId");

            migrationBuilder.RenameIndex(
                name: "IX_Stock_ProductoId",
                table: "Stocks",
                newName: "IX_Stocks_ProductoId");

            migrationBuilder.RenameIndex(
                name: "IX_Stock_ColorId",
                table: "Stocks",
                newName: "IX_Stocks_ColorId");

            migrationBuilder.RenameIndex(
                name: "IX_Producto_RubroId",
                table: "Productos",
                newName: "IX_Productos_RubroId");

            migrationBuilder.AddColumn<int>(
                name: "MarcaId",
                table: "Productos",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stocks",
                table: "Stocks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Productos",
                table: "Productos",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_MarcaId",
                table: "Productos",
                column: "MarcaId");

            migrationBuilder.AddForeignKey(
                name: "FK_LineaDeVentas_Stocks_StockId",
                table: "LineaDeVentas",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Marcas_MarcaId",
                table: "Productos",
                column: "MarcaId",
                principalTable: "Marcas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Rubros_RubroId",
                table: "Productos",
                column: "RubroId",
                principalTable: "Rubros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Colores_ColorId",
                table: "Stocks",
                column: "ColorId",
                principalTable: "Colores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Productos_ProductoId",
                table: "Stocks",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Sucursal_SucursalId",
                table: "Stocks",
                column: "SucursalId",
                principalTable: "Sucursal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Talles_TalleId",
                table: "Stocks",
                column: "TalleId",
                principalTable: "Talles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LineaDeVentas_Stocks_StockId",
                table: "LineaDeVentas");

            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Marcas_MarcaId",
                table: "Productos");

            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Rubros_RubroId",
                table: "Productos");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Colores_ColorId",
                table: "Stocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Productos_ProductoId",
                table: "Stocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Sucursal_SucursalId",
                table: "Stocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Talles_TalleId",
                table: "Stocks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stocks",
                table: "Stocks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Productos",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_MarcaId",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "MarcaId",
                table: "Productos");

            migrationBuilder.RenameTable(
                name: "Stocks",
                newName: "Stock");

            migrationBuilder.RenameTable(
                name: "Productos",
                newName: "Producto");

            migrationBuilder.RenameIndex(
                name: "IX_Stocks_TalleId",
                table: "Stock",
                newName: "IX_Stock_TalleId");

            migrationBuilder.RenameIndex(
                name: "IX_Stocks_SucursalId",
                table: "Stock",
                newName: "IX_Stock_SucursalId");

            migrationBuilder.RenameIndex(
                name: "IX_Stocks_ProductoId",
                table: "Stock",
                newName: "IX_Stock_ProductoId");

            migrationBuilder.RenameIndex(
                name: "IX_Stocks_ColorId",
                table: "Stock",
                newName: "IX_Stock_ColorId");

            migrationBuilder.RenameIndex(
                name: "IX_Productos_RubroId",
                table: "Producto",
                newName: "IX_Producto_RubroId");

            migrationBuilder.AddColumn<int>(
                name: "MarcaId",
                table: "Stock",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stock",
                table: "Stock",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Producto",
                table: "Producto",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_MarcaId",
                table: "Stock",
                column: "MarcaId");

            migrationBuilder.AddForeignKey(
                name: "FK_LineaDeVentas_Stock_StockId",
                table: "LineaDeVentas",
                column: "StockId",
                principalTable: "Stock",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Producto_Rubros_RubroId",
                table: "Producto",
                column: "RubroId",
                principalTable: "Rubros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stock_Colores_ColorId",
                table: "Stock",
                column: "ColorId",
                principalTable: "Colores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stock_Marcas_MarcaId",
                table: "Stock",
                column: "MarcaId",
                principalTable: "Marcas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stock_Producto_ProductoId",
                table: "Stock",
                column: "ProductoId",
                principalTable: "Producto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stock_Sucursal_SucursalId",
                table: "Stock",
                column: "SucursalId",
                principalTable: "Sucursal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stock_Talles_TalleId",
                table: "Stock",
                column: "TalleId",
                principalTable: "Talles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
