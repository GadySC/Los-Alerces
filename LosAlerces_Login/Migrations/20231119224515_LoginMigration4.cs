using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LosAlerces_Login.Migrations
{
    public partial class LoginMigration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuotationDate",
                table: "Cotizacion",
                newName: "quotationDate");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "Cotizacion",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                table: "Cotizacion");

            migrationBuilder.RenameColumn(
                name: "quotationDate",
                table: "Cotizacion",
                newName: "QuotationDate");
        }
    }
}
