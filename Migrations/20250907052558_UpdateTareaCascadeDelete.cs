using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkFlow_SIG10._1.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTareaCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tareas_Tareas_TareaPadreId",
                table: "Tareas");

            migrationBuilder.AddForeignKey(
                name: "FK_Tareas_Tareas_TareaPadreId",
                table: "Tareas",
                column: "TareaPadreId",
                principalTable: "Tareas",
                principalColumn: "TareaId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tareas_Tareas_TareaPadreId",
                table: "Tareas");

            migrationBuilder.AddForeignKey(
                name: "FK_Tareas_Tareas_TareaPadreId",
                table: "Tareas",
                column: "TareaPadreId",
                principalTable: "Tareas",
                principalColumn: "TareaId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
