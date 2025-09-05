using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkFlow_SIG10._1.Migrations
{
    /// <inheritdoc />
    public partial class AddRealDurationAndActionStateToTarea : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DuracionReal",
                table: "Tareas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EstadoAccion",
                table: "Tareas",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DuracionReal",
                table: "Tareas");

            migrationBuilder.DropColumn(
                name: "EstadoAccion",
                table: "Tareas");
        }
    }
}
