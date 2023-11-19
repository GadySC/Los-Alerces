using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LosAlerces_Login.Migrations
{
    public partial class LoginMigration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contactos_Cliente_ID_Cliente",
                table: "Contactos");

            migrationBuilder.AddForeignKey(
                name: "FK_Contactos_Cliente_ID_Cliente",
                table: "Contactos",
                column: "ID_Cliente",
                principalTable: "Cliente",
                principalColumn: "ID_Cliente");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contactos_Cliente_ID_Cliente",
                table: "Contactos");

            migrationBuilder.AddForeignKey(
                name: "FK_Contactos_Cliente_ID_Cliente",
                table: "Contactos",
                column: "ID_Cliente",
                principalTable: "Cliente",
                principalColumn: "ID_Cliente",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
