using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkFlow_SIG10._1.Migrations
{
    /// <inheritdoc />
    public partial class AddFinancialsToProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<decimal>(
                name: "PorcentajeUtilidad",
                table: "Proyectos",
                type: "decimal(5, 2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PorcentajeGastosGenerales",
                table: "Proyectos");

            migrationBuilder.DropColumn(
                name: "PorcentajeImpuesto",
                table: "Proyectos");

            migrationBuilder.DropColumn(
                name: "PorcentajeUtilidad",
                table: "Proyectos");
        }
    }
}
