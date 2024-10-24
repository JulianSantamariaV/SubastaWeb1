using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SubastaWeb.Migrations
{
    /// <inheritdoc />
    public partial class last : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductoModelDBOIdProducto",
                table: "UsuarioModelDBO",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioModelDBO_ProductoModelDBOIdProducto",
                table: "UsuarioModelDBO",
                column: "ProductoModelDBOIdProducto");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioModelDBO_Productos_ProductoModelDBOIdProducto",
                table: "UsuarioModelDBO",
                column: "ProductoModelDBOIdProducto",
                principalTable: "Productos",
                principalColumn: "IdProducto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioModelDBO_Productos_ProductoModelDBOIdProducto",
                table: "UsuarioModelDBO");

            migrationBuilder.DropIndex(
                name: "IX_UsuarioModelDBO_ProductoModelDBOIdProducto",
                table: "UsuarioModelDBO");

            migrationBuilder.DropColumn(
                name: "ProductoModelDBOIdProducto",
                table: "UsuarioModelDBO");
        }
    }
}
