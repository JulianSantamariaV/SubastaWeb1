using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SubastaWeb.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    IdProducto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Categoria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoDeSubasta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrecioInicial = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    PrecioFinal = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    FechaCaducidad = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EnLiquidacion = table.Column<bool>(type: "bit", nullable: false),
                    ProductoTipo = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.IdProducto);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Productos");
        }
    }
}
