using Microsoft.EntityFrameworkCore.Migrations;

namespace LaTienda.API.Migrations
{
    public partial class AddTipoTalle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Iva",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "NetoGravado",
                table: "Productos");

            migrationBuilder.RenameColumn(
                name: "PrecioVenta",
                table: "Productos",
                newName: "PorcentajeIva");

            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "Ventas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoTalle",
                table: "Talles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TipoTalle",
                table: "Productos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_ClienteId",
                table: "Ventas",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ventas_Clientes_ClienteId",
                table: "Ventas",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ventas_Clientes_ClienteId",
                table: "Ventas");

            migrationBuilder.DropIndex(
                name: "IX_Ventas_ClienteId",
                table: "Ventas");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Ventas");

            migrationBuilder.DropColumn(
                name: "TipoTalle",
                table: "Talles");

            migrationBuilder.DropColumn(
                name: "TipoTalle",
                table: "Productos");

            migrationBuilder.RenameColumn(
                name: "PorcentajeIva",
                table: "Productos",
                newName: "PrecioVenta");

            migrationBuilder.AddColumn<double>(
                name: "Iva",
                table: "Productos",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "NetoGravado",
                table: "Productos",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
