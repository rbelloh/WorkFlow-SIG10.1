using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkFlow_SIG10._1.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEstadoDePagoItemModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EstadosDePagoItem_Tareas_TareaId",
                table: "EstadosDePagoItem");

            migrationBuilder.DropColumn(
                name: "WBS",
                table: "EstadosDePagoItem");

            migrationBuilder.RenameColumn(
                name: "NombreTarea",
                table: "EstadosDePagoItem",
                newName: "Descripcion");

            migrationBuilder.AlterColumn<int>(
                name: "TareaId",
                table: "EstadosDePagoItem",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ItemPresupuestoId",
                table: "EstadosDePagoItem",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EstadosDePagoItem_ItemPresupuestoId",
                table: "EstadosDePagoItem",
                column: "ItemPresupuestoId");

            migrationBuilder.AddForeignKey(
                name: "FK_EstadosDePagoItem_ItemsPresupuesto_ItemPresupuestoId",
                table: "EstadosDePagoItem",
                column: "ItemPresupuestoId",
                principalTable: "ItemsPresupuesto",
                principalColumn: "ItemPresupuestoId");

            migrationBuilder.AddForeignKey(
                name: "FK_EstadosDePagoItem_Tareas_TareaId",
                table: "EstadosDePagoItem",
                column: "TareaId",
                principalTable: "Tareas",
                principalColumn: "TareaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EstadosDePagoItem_ItemsPresupuesto_ItemPresupuestoId",
                table: "EstadosDePagoItem");

            migrationBuilder.DropForeignKey(
                name: "FK_EstadosDePagoItem_Tareas_TareaId",
                table: "EstadosDePagoItem");

            migrationBuilder.DropIndex(
                name: "IX_EstadosDePagoItem_ItemPresupuestoId",
                table: "EstadosDePagoItem");

            migrationBuilder.DropColumn(
                name: "ItemPresupuestoId",
                table: "EstadosDePagoItem");

            migrationBuilder.RenameColumn(
                name: "Descripcion",
                table: "EstadosDePagoItem",
                newName: "NombreTarea");

            migrationBuilder.AlterColumn<int>(
                name: "TareaId",
                table: "EstadosDePagoItem",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WBS",
                table: "EstadosDePagoItem",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_EstadosDePagoItem_Tareas_TareaId",
                table: "EstadosDePagoItem",
                column: "TareaId",
                principalTable: "Tareas",
                principalColumn: "TareaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
