using Microsoft.EntityFrameworkCore.Migrations;

namespace LaTienda.API.Migrations
{
    public partial class ComprobanteUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Comprobantes");

            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "Comprobantes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Tipo",
                table: "Comprobantes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Comprobantes_ClienteId",
                table: "Comprobantes",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comprobantes_Clientes_ClienteId",
                table: "Comprobantes",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comprobantes_Clientes_ClienteId",
                table: "Comprobantes");

            migrationBuilder.DropIndex(
                name: "IX_Comprobantes_ClienteId",
                table: "Comprobantes");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Comprobantes");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Comprobantes");

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Comprobantes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
