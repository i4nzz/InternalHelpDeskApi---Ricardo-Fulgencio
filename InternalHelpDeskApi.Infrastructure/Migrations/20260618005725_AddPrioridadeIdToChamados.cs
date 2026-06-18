using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternalHelpDeskApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPrioridadeIdToChamados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrioridadeId",
                table: "Chamados",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chamados_PrioridadeId",
                table: "Chamados",
                column: "PrioridadeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chamados_Prioridades_PrioridadeId",
                table: "Chamados",
                column: "PrioridadeId",
                principalTable: "Prioridades",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chamados_Prioridades_PrioridadeId",
                table: "Chamados");

            migrationBuilder.DropIndex(
                name: "IX_Chamados_PrioridadeId",
                table: "Chamados");

            migrationBuilder.DropColumn(
                name: "PrioridadeId",
                table: "Chamados");
        }
    }
}
