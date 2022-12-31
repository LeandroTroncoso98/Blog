using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogCore.AccesoDatos.Migrations
{
    public partial class SolucionFechaCreacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaDeCreacion",
                table: "Articulo");

            migrationBuilder.AddColumn<string>(
                name: "FechaCreacion",
                table: "Articulo",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Articulo");

            migrationBuilder.AddColumn<string>(
                name: "FechaDeCreacion",
                table: "Articulo",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
