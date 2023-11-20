using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LosAlerces_DBManagement.Migrations
{
    public partial class initialBDDBManagement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    ID_Cliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    phone = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ContactoName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ContactoLastname = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ContactoEmail = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ContactoPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.ID_Cliente);
                });

            migrationBuilder.CreateTable(
                name: "Personal",
                columns: table => new
                {
                    ID_Personal = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    lastname = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    profession = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    salary = table.Column<decimal>(type: "DECIMAL(10,2)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    phone = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personal", x => x.ID_Personal);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    ID_Productos = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    note = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    price = table.Column<decimal>(type: "DECIMAL(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.ID_Productos);
                });

            migrationBuilder.CreateTable(
                name: "Cotizacion",
                columns: table => new
                {
                    ID_Cotizacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Cliente = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    quotationDate = table.Column<DateTime>(type: "DATE", nullable: false),
                    quantityofproduct = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cotizacion", x => x.ID_Cotizacion);
                    table.ForeignKey(
                        name: "FK_Cotizacion_Cliente_ID_Cliente",
                        column: x => x.ID_Cliente,
                        principalTable: "Cliente",
                        principalColumn: "ID_Cliente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalCotizacion",
                columns: table => new
                {
                    ID_Cotizacion = table.Column<int>(type: "int", nullable: false),
                    ID_Personal = table.Column<int>(type: "int", nullable: false),
                    PersonalID_Personal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalCotizacion", x => new { x.ID_Cotizacion, x.ID_Personal });
                    table.ForeignKey(
                        name: "FK_PersonalCotizacion_Cotizacion_ID_Cotizacion",
                        column: x => x.ID_Cotizacion,
                        principalTable: "Cotizacion",
                        principalColumn: "ID_Cotizacion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonalCotizacion_Personal_PersonalID_Personal",
                        column: x => x.PersonalID_Personal,
                        principalTable: "Personal",
                        principalColumn: "ID_Personal",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductoCotizacion",
                columns: table => new
                {
                    ID_Cotizacion = table.Column<int>(type: "int", nullable: false),
                    ID_Producto = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    ProductoID_Productos = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoCotizacion", x => new { x.ID_Cotizacion, x.ID_Producto });
                    table.ForeignKey(
                        name: "FK_ProductoCotizacion_Cotizacion_ID_Cotizacion",
                        column: x => x.ID_Cotizacion,
                        principalTable: "Cotizacion",
                        principalColumn: "ID_Cotizacion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductoCotizacion_Productos_ProductoID_Productos",
                        column: x => x.ProductoID_Productos,
                        principalTable: "Productos",
                        principalColumn: "ID_Productos",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cotizacion_ID_Cliente",
                table: "Cotizacion",
                column: "ID_Cliente");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalCotizacion_PersonalID_Personal",
                table: "PersonalCotizacion",
                column: "PersonalID_Personal");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoCotizacion_ProductoID_Productos",
                table: "ProductoCotizacion",
                column: "ProductoID_Productos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonalCotizacion");

            migrationBuilder.DropTable(
                name: "ProductoCotizacion");

            migrationBuilder.DropTable(
                name: "Personal");

            migrationBuilder.DropTable(
                name: "Cotizacion");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Cliente");
        }
    }
}
