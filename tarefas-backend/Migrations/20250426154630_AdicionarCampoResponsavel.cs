using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tarefas_backend.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarCampoResponsavel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Responsavel",
                table: "Tarefas",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Responsavel",
                table: "Tarefas");
        }
    }
}
