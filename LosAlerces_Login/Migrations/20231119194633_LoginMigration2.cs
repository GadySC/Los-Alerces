using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LosAlerces_Login.Migrations
{
    public partial class LoginMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Contactos_ID_Cliente",
                table: "Contactos");

            migrationBuilder.CreateIndex(
                name: "IX_Contactos_ID_Cliente",
                table: "Contactos",
                column: "ID_Cliente",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Contactos_ID_Cliente",
                table: "Contactos");

            migrationBuilder.CreateIndex(
                name: "IX_Contactos_ID_Cliente",
                table: "Contactos",
                column: "ID_Cliente");
        }
    }
}
