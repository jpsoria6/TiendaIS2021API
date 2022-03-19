using Microsoft.EntityFrameworkCore.Migrations;

namespace LaTienda.API.Migrations
{
    public partial class ChangeFacturaComprobanteVenta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ventas_Comprobantes_FacturaId",
                table: "Ventas");

            migrationBuilder.RenameColumn(
                name: "FacturaId",
                table: "Ventas",
                newName: "ComprobanteId");

            migrationBuilder.RenameIndex(
                name: "IX_Ventas_FacturaId",
                table: "Ventas",
                newName: "IX_Ventas_ComprobanteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ventas_Comprobantes_ComprobanteId",
                table: "Ventas",
                column: "ComprobanteId",
                principalTable: "Comprobantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ventas_Comprobantes_ComprobanteId",
                table: "Ventas");

            migrationBuilder.RenameColumn(
                name: "ComprobanteId",
                table: "Ventas",
                newName: "FacturaId");

            migrationBuilder.RenameIndex(
                name: "IX_Ventas_ComprobanteId",
                table: "Ventas",
                newName: "IX_Ventas_FacturaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ventas_Comprobantes_FacturaId",
                table: "Ventas",
                column: "FacturaId",
                principalTable: "Comprobantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
