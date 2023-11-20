using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LosAlerces_Login.Migrations
{
    public partial class DATABASEFOREIGNKEYCorrection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalCotizacion_Personal_PersonalID_Personal",
                table: "PersonalCotizacion");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductoCotizacion_Productos_ProductoID_Productos",
                table: "ProductoCotizacion");

            migrationBuilder.DropIndex(
                name: "IX_ProductoCotizacion_ProductoID_Productos",
                table: "ProductoCotizacion");

            migrationBuilder.DropIndex(
                name: "IX_PersonalCotizacion_PersonalID_Personal",
                table: "PersonalCotizacion");

            migrationBuilder.DropColumn(
                name: "ProductoID_Productos",
                table: "ProductoCotizacion");

            migrationBuilder.DropColumn(
                name: "PersonalID_Personal",
                table: "PersonalCotizacion");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoCotizacion_ID_Producto",
                table: "ProductoCotizacion",
                column: "ID_Producto");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalCotizacion_ID_Personal",
                table: "PersonalCotizacion",
                column: "ID_Personal");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalCotizacion_Personal_ID_Personal",
                table: "PersonalCotizacion",
                column: "ID_Personal",
                principalTable: "Personal",
                principalColumn: "ID_Personal",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductoCotizacion_Productos_ID_Producto",
                table: "ProductoCotizacion",
                column: "ID_Producto",
                principalTable: "Productos",
                principalColumn: "ID_Productos",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalCotizacion_Personal_ID_Personal",
                table: "PersonalCotizacion");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductoCotizacion_Productos_ID_Producto",
                table: "ProductoCotizacion");

            migrationBuilder.DropIndex(
                name: "IX_ProductoCotizacion_ID_Producto",
                table: "ProductoCotizacion");

            migrationBuilder.DropIndex(
                name: "IX_PersonalCotizacion_ID_Personal",
                table: "PersonalCotizacion");

            migrationBuilder.AddColumn<int>(
                name: "ProductoID_Productos",
                table: "ProductoCotizacion",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PersonalID_Personal",
                table: "PersonalCotizacion",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductoCotizacion_ProductoID_Productos",
                table: "ProductoCotizacion",
                column: "ProductoID_Productos");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalCotizacion_PersonalID_Personal",
                table: "PersonalCotizacion",
                column: "PersonalID_Personal");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalCotizacion_Personal_PersonalID_Personal",
                table: "PersonalCotizacion",
                column: "PersonalID_Personal",
                principalTable: "Personal",
                principalColumn: "ID_Personal",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductoCotizacion_Productos_ProductoID_Productos",
                table: "ProductoCotizacion",
                column: "ProductoID_Productos",
                principalTable: "Productos",
                principalColumn: "ID_Productos",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
