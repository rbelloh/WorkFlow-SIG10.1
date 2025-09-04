using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkFlow_SIG10._1.Migrations
{
    /// <inheritdoc />
    public partial class AddDetailedProjectFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PresupuestoTotal",
                table: "Proyectos");

            migrationBuilder.RenameColumn(
                name: "Ubicacion",
                table: "Proyectos",
                newName: "Region");

            migrationBuilder.RenameColumn(
                name: "NombreProyecto",
                table: "Proyectos",
                newName: "Pais");

            migrationBuilder.RenameColumn(
                name: "FechaInicio",
                table: "Proyectos",
                newName: "FechaTerminoProyecto");

            migrationBuilder.RenameColumn(
                name: "FechaFinEstimada",
                table: "Proyectos",
                newName: "FechaInicioProyecto");

            migrationBuilder.AddColumn<string>(
                name: "Ciudad",
                table: "Proyectos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CiudadEmpresaEjecutora",
                table: "Proyectos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CiudadEmpresaMandante",
                table: "Proyectos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CodigoObra",
                table: "Proyectos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "DireccionEmpresaEjecutora",
                table: "Proyectos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "DireccionEmpresaMandante",
                table: "Proyectos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "IdEmpresaEjecutora",
                table: "Proyectos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "IdEmpresaMandante",
                table: "Proyectos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "IdRepresentanteLegalEjecutora",
                table: "Proyectos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "IdRepresentanteLegalMandante",
                table: "Proyectos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "LogoEmpresaEjecutoraPath",
                table: "Proyectos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "LogoEmpresaMandantePath",
                table: "Proyectos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "NombreAdministradorObraEjecutora",
                table: "Proyectos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "NombreAdministradorObraMandante",
                table: "Proyectos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "NombreEmpresaEjecutora",
                table: "Proyectos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "NombreEmpresaMandante",
                table: "Proyectos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "NombreObra",
                table: "Proyectos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "NombreRepresentanteLegalEjecutora",
                table: "Proyectos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "NombreRepresentanteLegalMandante",
                table: "Proyectos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ciudad",
                table: "Proyectos");

            migrationBuilder.DropColumn(
                name: "CiudadEmpresaEjecutora",
                table: "Proyectos");

            migrationBuilder.DropColumn(
                name: "CiudadEmpresaMandante",
                table: "Proyectos");

            migrationBuilder.DropColumn(
                name: "CodigoObra",
                table: "Proyectos");

            migrationBuilder.DropColumn(
                name: "DireccionEmpresaEjecutora",
                table: "Proyectos");

            migrationBuilder.DropColumn(
                name: "DireccionEmpresaMandante",
                table: "Proyectos");

            migrationBuilder.DropColumn(
                name: "IdEmpresaEjecutora",
                table: "Proyectos");

            migrationBuilder.DropColumn(
                name: "IdEmpresaMandante",
                table: "Proyectos");

            migrationBuilder.DropColumn(
                name: "IdRepresentanteLegalEjecutora",
                table: "Proyectos");

            migrationBuilder.DropColumn(
                name: "IdRepresentanteLegalMandante",
                table: "Proyectos");

            migrationBuilder.DropColumn(
                name: "LogoEmpresaEjecutoraPath",
                table: "Proyectos");

            migrationBuilder.DropColumn(
                name: "LogoEmpresaMandantePath",
                table: "Proyectos");

            migrationBuilder.DropColumn(
                name: "NombreAdministradorObraEjecutora",
                table: "Proyectos");

            migrationBuilder.DropColumn(
                name: "NombreAdministradorObraMandante",
                table: "Proyectos");

            migrationBuilder.DropColumn(
                name: "NombreEmpresaEjecutora",
                table: "Proyectos");

            migrationBuilder.DropColumn(
                name: "NombreEmpresaMandante",
                table: "Proyectos");

            migrationBuilder.DropColumn(
                name: "NombreObra",
                table: "Proyectos");

            migrationBuilder.DropColumn(
                name: "NombreRepresentanteLegalEjecutora",
                table: "Proyectos");

            migrationBuilder.DropColumn(
                name: "NombreRepresentanteLegalMandante",
                table: "Proyectos");

            migrationBuilder.RenameColumn(
                name: "Region",
                table: "Proyectos",
                newName: "Ubicacion");

            migrationBuilder.RenameColumn(
                name: "Pais",
                table: "Proyectos",
                newName: "NombreProyecto");

            migrationBuilder.RenameColumn(
                name: "FechaTerminoProyecto",
                table: "Proyectos",
                newName: "FechaInicio");

            migrationBuilder.RenameColumn(
                name: "FechaInicioProyecto",
                table: "Proyectos",
                newName: "FechaFinEstimada");

            migrationBuilder.AddColumn<decimal>(
                name: "PresupuestoTotal",
                table: "Proyectos",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
