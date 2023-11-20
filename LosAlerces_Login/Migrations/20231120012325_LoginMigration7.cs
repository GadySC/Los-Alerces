using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LosAlerces_Login.Migrations
{
    public partial class LoginMigration7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cliente_Contactos_idContacto",
                table: "Cliente");

            migrationBuilder.DropTable(
                name: "Contactos");

            migrationBuilder.DropIndex(
                name: "IX_Cliente_idContacto",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "idContacto",
                table: "Cliente");

            migrationBuilder.AddColumn<string>(
                name: "ContactoEmail",
                table: "Cliente",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContactoLastname",
                table: "Cliente",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContactoName",
                table: "Cliente",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContactoPhone",
                table: "Cliente",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactoEmail",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "ContactoLastname",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "ContactoName",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "ContactoPhone",
                table: "Cliente");

            migrationBuilder.AddColumn<int>(
                name: "idContacto",
                table: "Cliente",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Contactos",
                columns: table => new
                {
                    ID_Contactos = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Cliente = table.Column<int>(type: "int", nullable: false),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    lastname = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contactos", x => x.ID_Contactos);
                });

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
    }
}
