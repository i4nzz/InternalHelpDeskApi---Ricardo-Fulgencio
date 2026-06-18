using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternalHelpDeskApi.Infrastructure.Migrations
{
    public partial class RemoveUniquePrioridadeConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Prioridades_CategoriaId",
                table: "Prioridades");

            migrationBuilder.CreateIndex(
                name: "IX_Prioridades_CategoriaId",
                table: "Prioridades",
                column: "CategoriaId",
                unique: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Prioridades_CategoriaId",
                table: "Prioridades");

            migrationBuilder.CreateIndex(
                name: "IX_Prioridades_CategoriaId",
                table: "Prioridades",
                column: "CategoriaId",
                unique: true);
        }
    }
}
