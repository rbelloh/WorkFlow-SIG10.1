using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkFlow_SIG10._1.Migrations
{
    /// <inheritdoc />
    public partial class AddEstadoDePagoModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EstadosDePago",
                columns: table => new
                {
                    EstadoDePagoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProyectoId = table.Column<int>(type: "int", nullable: false),
                    NumeroEP = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Periodo = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Estado = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TotalContratoOriginalNeto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmpliacionesNeto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPenalizacionesNeto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalContratoActualizadoNeto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AvancePeriodoNeto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RetencionPeriodoNeto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImpuestoIvaRetencion = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalImporteFacturacionAvanceMensual = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RetencionesAcumuladasNeto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalImporteAcumuladoNeto = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadosDePago", x => x.EstadoDePagoId);
                    table.ForeignKey(
                        name: "FK_EstadosDePago_Proyectos_ProyectoId",
                        column: x => x.ProyectoId,
                        principalTable: "Proyectos",
                        principalColumn: "ProyectoID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EstadosDePagoItem",
                columns: table => new
                {
                    EstadoDePagoItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EstadoDePagoId = table.Column<int>(type: "int", nullable: false),
                    TareaId = table.Column<int>(type: "int", nullable: false),
                    NombreTarea = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WBS = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Unidad = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CantidadContrato = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImporteContrato = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CantidadAvancePeriodo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImporteAvancePeriodo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CantidadAvanceAcumulado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImporteAvanceAcumulado = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadosDePagoItem", x => x.EstadoDePagoItemId);
                    table.ForeignKey(
                        name: "FK_EstadosDePagoItem_EstadosDePago_EstadoDePagoId",
                        column: x => x.EstadoDePagoId,
                        principalTable: "EstadosDePago",
                        principalColumn: "EstadoDePagoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EstadosDePagoItem_Tareas_TareaId",
                        column: x => x.TareaId,
                        principalTable: "Tareas",
                        principalColumn: "TareaId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_EstadosDePago_ProyectoId",
                table: "EstadosDePago",
                column: "ProyectoId");

            migrationBuilder.CreateIndex(
                name: "IX_EstadosDePagoItem_EstadoDePagoId",
                table: "EstadosDePagoItem",
                column: "EstadoDePagoId");

            migrationBuilder.CreateIndex(
                name: "IX_EstadosDePagoItem_TareaId",
                table: "EstadosDePagoItem",
                column: "TareaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EstadosDePagoItem");

            migrationBuilder.DropTable(
                name: "EstadosDePago");
        }
    }
}
