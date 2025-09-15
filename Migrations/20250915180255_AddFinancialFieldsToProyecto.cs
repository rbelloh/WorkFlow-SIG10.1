using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkFlow_SIG10._1.Migrations
{
    /// <inheritdoc />
    public partial class AddFinancialFieldsToProyecto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PorcentajeGastosGenerales",
                table: "Proyectos");

            migrationBuilder.DropColumn(
                name: "PorcentajeImpuesto",
                table: "Proyectos");

            migrationBuilder.RenameColumn(
                name: "PorcentajeUtilidad",
                table: "Proyectos",
                newName: "IVAPorcentaje");

            migrationBuilder.AddColumn<int>(
                name: "TipoCostosIndirectos",
                table: "Proyectos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TipoGastosGenerales",
                table: "Proyectos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TipoUtilidad",
                table: "Proyectos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorCostosIndirectos",
                table: "Proyectos",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorGastosGenerales",
                table: "Proyectos",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorUtilidad",
                table: "Proyectos",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoCostosIndirectos",
                table: "Proyectos");

            migrationBuilder.DropColumn(
                name: "TipoGastosGenerales",
                table: "Proyectos");

            migrationBuilder.DropColumn(
                name: "TipoUtilidad",
                table: "Proyectos");

            migrationBuilder.DropColumn(
                name: "ValorCostosIndirectos",
                table: "Proyectos");

            migrationBuilder.DropColumn(
                name: "ValorGastosGenerales",
                table: "Proyectos");

            migrationBuilder.DropColumn(
                name: "ValorUtilidad",
                table: "Proyectos");

            migrationBuilder.RenameColumn(
                name: "IVAPorcentaje",
                table: "Proyectos",
                newName: "PorcentajeUtilidad");

            migrationBuilder.AddColumn<decimal>(
                name: "PorcentajeGastosGenerales",
                table: "Proyectos",
                type: "decimal(5, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PorcentajeImpuesto",
                table: "Proyectos",
                type: "decimal(5, 2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
