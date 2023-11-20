using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LosAlerces_Login.Migrations
{
    public partial class LoginMigration5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contactos_Cliente_ID_Cliente",
                table: "Contactos");

            migrationBuilder.DropIndex(
                name: "IX_Contactos_ID_Cliente",
                table: "Contactos");

            migrationBuilder.AddColumn<int>(
                name: "idContacto",
                table: "Cliente",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_idContacto",
                table: "Cliente",
                column: "idContacto",
                unique: true,
                filter: "[idContacto] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Cliente_Contactos_idContacto",
                table: "Cliente",
                column: "idContacto",
                principalTable: "Contactos",
                principalColumn: "ID_Contactos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cliente_Contactos_idContacto",
                table: "Cliente");

            migrationBuilder.DropIndex(
                name: "IX_Cliente_idContacto",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "idContacto",
                table: "Cliente");

            migrationBuilder.CreateIndex(
                name: "IX_Contactos_ID_Cliente",
                table: "Contactos",
                column: "ID_Cliente",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contactos_Cliente_ID_Cliente",
                table: "Contactos",
                column: "ID_Cliente",
                principalTable: "Cliente",
                principalColumn: "ID_Cliente");
        }
    }
}
