using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Data.Migrations
{
    public partial class KategoriemigrationV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kat",
                table: "Kontakt");

            migrationBuilder.AddColumn<string>(
                name: "Kategoria",
                table: "Kontakt",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kategoria",
                table: "Kontakt");

            migrationBuilder.AddColumn<int>(
                name: "Kat",
                table: "Kontakt",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
