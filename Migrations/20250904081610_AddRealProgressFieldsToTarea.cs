using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkFlow_SIG10._1.Migrations
{
    /// <inheritdoc />
    public partial class AddRealProgressFieldsToTarea : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaFinReal",
                table: "Tareas",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaInicioReal",
                table: "Tareas",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notas",
                table: "Tareas",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "PorcentajeCompletadoReal",
                table: "Tareas",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaFinReal",
                table: "Tareas");

            migrationBuilder.DropColumn(
                name: "FechaInicioReal",
                table: "Tareas");

            migrationBuilder.DropColumn(
                name: "Notas",
                table: "Tareas");

            migrationBuilder.DropColumn(
                name: "PorcentajeCompletadoReal",
                table: "Tareas");
        }
    }
}
