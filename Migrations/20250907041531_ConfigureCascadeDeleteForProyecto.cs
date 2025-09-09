using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkFlow_SIG10._1.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureCascadeDeleteForProyecto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notificaciones_Proyectos_ProyectoId",
                table: "Notificaciones");

            migrationBuilder.AddForeignKey(
                name: "FK_Notificaciones_Proyectos_ProyectoId",
                table: "Notificaciones",
                column: "ProyectoId",
                principalTable: "Proyectos",
                principalColumn: "ProyectoID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notificaciones_Proyectos_ProyectoId",
                table: "Notificaciones");

            migrationBuilder.AddForeignKey(
                name: "FK_Notificaciones_Proyectos_ProyectoId",
                table: "Notificaciones",
                column: "ProyectoId",
                principalTable: "Proyectos",
                principalColumn: "ProyectoID");
        }
    }
}
