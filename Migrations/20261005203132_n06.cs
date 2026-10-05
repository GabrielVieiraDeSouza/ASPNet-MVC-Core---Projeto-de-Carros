using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projeto_Carros.Migrations
{
    /// <inheritdoc />
    public partial class n06 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Apelido",
                table: "Veiculos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Apelido",
                table: "Veiculos",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
