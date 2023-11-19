using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LosAlerces_DBManagement.Migrations
{
    public partial class DBManagementMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    ID_Cliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    phone = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.ID_Cliente);
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
                name: "Contactos",
                columns: table => new
                {
                    ID_Contactos = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Cliente = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    lastname = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contactos", x => x.ID_Contactos);
                    table.ForeignKey(
                        name: "FK_Contactos_Clientes_ID_Cliente",
                        column: x => x.ID_Cliente,
                        principalTable: "Clientes",
                        principalColumn: "ID_Cliente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cotizaciones",
                columns: table => new
                {
                    ID_Cotizacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Cliente = table.Column<int>(type: "int", nullable: false),
                    QuotationDate = table.Column<DateTime>(type: "DATE", nullable: false),
                    quantityofproduct = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cotizaciones", x => x.ID_Cotizacion);
                    table.ForeignKey(
                        name: "FK_Cotizaciones_Clientes_ID_Cliente",
                        column: x => x.ID_Cliente,
                        principalTable: "Clientes",
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
                        name: "FK_PersonalCotizacion_Cotizaciones_ID_Cotizacion",
                        column: x => x.ID_Cotizacion,
                        principalTable: "Cotizaciones",
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
                name: "ProductosCotizacion",
                columns: table => new
                {
                    ID_Cotizacion = table.Column<int>(type: "int", nullable: false),
                    ID_Producto = table.Column<int>(type: "int", nullable: false),
                    ProductoID_Productos = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductosCotizacion", x => new { x.ID_Cotizacion, x.ID_Producto });
                    table.ForeignKey(
                        name: "FK_ProductosCotizacion_Cotizaciones_ID_Cotizacion",
                        column: x => x.ID_Cotizacion,
                        principalTable: "Cotizaciones",
                        principalColumn: "ID_Cotizacion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductosCotizacion_Productos_ProductoID_Productos",
                        column: x => x.ProductoID_Productos,
                        principalTable: "Productos",
                        principalColumn: "ID_Productos",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contactos_ID_Cliente",
                table: "Contactos",
                column: "ID_Cliente");

            migrationBuilder.CreateIndex(
                name: "IX_Cotizaciones_ID_Cliente",
                table: "Cotizaciones",
                column: "ID_Cliente");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalCotizacion_PersonalID_Personal",
                table: "PersonalCotizacion",
                column: "PersonalID_Personal");

            migrationBuilder.CreateIndex(
                name: "IX_ProductosCotizacion_ProductoID_Productos",
                table: "ProductosCotizacion",
                column: "ProductoID_Productos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contactos");

            migrationBuilder.DropTable(
                name: "PersonalCotizacion");

            migrationBuilder.DropTable(
                name: "ProductosCotizacion");

            migrationBuilder.DropTable(
                name: "Personal");

            migrationBuilder.DropTable(
                name: "Cotizaciones");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
