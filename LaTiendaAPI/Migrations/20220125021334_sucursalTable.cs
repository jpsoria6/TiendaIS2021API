using Microsoft.EntityFrameworkCore.Migrations;

namespace LaTienda.API.Migrations
{
    public partial class sucursalTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empleados_Sucursal_SucursalId",
                table: "Empleados");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Sucursal_SucursalId",
                table: "Stocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Sucursal_Tiendas_TiendaId",
                table: "Sucursal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sucursal",
                table: "Sucursal");

            migrationBuilder.RenameTable(
                name: "Sucursal",
                newName: "Sucursales");

            migrationBuilder.RenameIndex(
                name: "IX_Sucursal_TiendaId",
                table: "Sucursales",
                newName: "IX_Sucursales_TiendaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sucursales",
                table: "Sucursales",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Empleados_Sucursales_SucursalId",
                table: "Empleados",
                column: "SucursalId",
                principalTable: "Sucursales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Sucursales_SucursalId",
                table: "Stocks",
                column: "SucursalId",
                principalTable: "Sucursales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sucursales_Tiendas_TiendaId",
                table: "Sucursales",
                column: "TiendaId",
                principalTable: "Tiendas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empleados_Sucursales_SucursalId",
                table: "Empleados");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Sucursales_SucursalId",
                table: "Stocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Sucursales_Tiendas_TiendaId",
                table: "Sucursales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sucursales",
                table: "Sucursales");

            migrationBuilder.RenameTable(
                name: "Sucursales",
                newName: "Sucursal");

            migrationBuilder.RenameIndex(
                name: "IX_Sucursales_TiendaId",
                table: "Sucursal",
                newName: "IX_Sucursal_TiendaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sucursal",
                table: "Sucursal",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Empleados_Sucursal_SucursalId",
                table: "Empleados",
                column: "SucursalId",
                principalTable: "Sucursal",
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
                name: "FK_Sucursal_Tiendas_TiendaId",
                table: "Sucursal",
                column: "TiendaId",
                principalTable: "Tiendas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
