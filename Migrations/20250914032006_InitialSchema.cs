using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkFlow_SIG10._1.Migrations
{
    /// <inheritdoc />
    public partial class InitialSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Apellido = table.Column<string>(type: "TEXT", nullable: false),
                    Direccion = table.Column<string>(type: "TEXT", nullable: false),
                    NumeroIdentificacion = table.Column<string>(type: "TEXT", nullable: false),
                    Dependencia = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Inventarios",
                columns: table => new
                {
                    MaterialID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NombreMaterial = table.Column<string>(type: "TEXT", nullable: false),
                    CantidadStock = table.Column<int>(type: "INTEGER", nullable: false),
                    UnidadMedida = table.Column<string>(type: "TEXT", nullable: false),
                    UbicacionBodega = table.Column<string>(type: "TEXT", nullable: false),
                    StockMinimo = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventarios", x => x.MaterialID);
                });

            migrationBuilder.CreateTable(
                name: "Proyectos",
                columns: table => new
                {
                    ProyectoID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CodigoObra = table.Column<string>(type: "TEXT", nullable: false),
                    NombreObra = table.Column<string>(type: "TEXT", nullable: false),
                    NombreEmpresaEjecutora = table.Column<string>(type: "TEXT", nullable: false),
                    IdEmpresaEjecutora = table.Column<string>(type: "TEXT", nullable: false),
                    DireccionEmpresaEjecutora = table.Column<string>(type: "TEXT", nullable: false),
                    CiudadEmpresaEjecutora = table.Column<string>(type: "TEXT", nullable: false),
                    NombreRepresentanteLegalEjecutora = table.Column<string>(type: "TEXT", nullable: false),
                    IdRepresentanteLegalEjecutora = table.Column<string>(type: "TEXT", nullable: false),
                    NombreAdministradorObraEjecutora = table.Column<string>(type: "TEXT", nullable: false),
                    Pais = table.Column<string>(type: "TEXT", nullable: false),
                    Region = table.Column<string>(type: "TEXT", nullable: false),
                    Ciudad = table.Column<string>(type: "TEXT", nullable: false),
                    NombreEmpresaMandante = table.Column<string>(type: "TEXT", nullable: false),
                    IdEmpresaMandante = table.Column<string>(type: "TEXT", nullable: false),
                    DireccionEmpresaMandante = table.Column<string>(type: "TEXT", nullable: false),
                    CiudadEmpresaMandante = table.Column<string>(type: "TEXT", nullable: false),
                    NombreRepresentanteLegalMandante = table.Column<string>(type: "TEXT", nullable: false),
                    IdRepresentanteLegalMandante = table.Column<string>(type: "TEXT", nullable: false),
                    NombreAdministradorObraMandante = table.Column<string>(type: "TEXT", nullable: false),
                    FechaInicioProyecto = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FechaTerminoProyecto = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LogoEmpresaEjecutoraPath = table.Column<string>(type: "TEXT", nullable: false),
                    LogoEmpresaMandantePath = table.Column<string>(type: "TEXT", nullable: false),
                    Estado = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proyectos", x => x.ProyectoID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AmpliacionesProyectos",
                columns: table => new
                {
                    AmpliacionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProyectoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmpliacionesProyectos", x => x.AmpliacionId);
                    table.ForeignKey(
                        name: "FK_AmpliacionesProyectos_Proyectos_ProyectoId",
                        column: x => x.ProyectoId,
                        principalTable: "Proyectos",
                        principalColumn: "ProyectoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contratos",
                columns: table => new
                {
                    ContratoID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProyectoID = table.Column<int>(type: "INTEGER", nullable: false),
                    TipoContrato = table.Column<string>(type: "TEXT", nullable: false),
                    ValorContrato = table.Column<decimal>(type: "TEXT", nullable: false),
                    FechaFirma = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Estado = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contratos", x => x.ContratoID);
                    table.ForeignKey(
                        name: "FK_Contratos_Proyectos_ProyectoID",
                        column: x => x.ProyectoID,
                        principalTable: "Proyectos",
                        principalColumn: "ProyectoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ControlesCalidad",
                columns: table => new
                {
                    ControlID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProyectoID = table.Column<int>(type: "INTEGER", nullable: false),
                    DescripcionInspeccion = table.Column<string>(type: "TEXT", nullable: false),
                    Resultado = table.Column<string>(type: "TEXT", nullable: false),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlesCalidad", x => x.ControlID);
                    table.ForeignKey(
                        name: "FK_ControlesCalidad_Proyectos_ProyectoID",
                        column: x => x.ProyectoID,
                        principalTable: "Proyectos",
                        principalColumn: "ProyectoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EstadosDePago",
                columns: table => new
                {
                    EstadoDePagoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProyectoId = table.Column<int>(type: "INTEGER", nullable: false),
                    NumeroEP = table.Column<int>(type: "INTEGER", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Periodo = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Estado = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    TotalContratoOriginalNeto = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    TotalAmpliacionesNeto = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    TotalPenalizacionesNeto = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    TotalContratoActualizadoNeto = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    AvancePeriodoNeto = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    RetencionPeriodoNeto = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    ImpuestoIvaRetencion = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    TotalImporteFacturacionAvanceMensual = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    RetencionesAcumuladasNeto = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    TotalImporteAcumuladoNeto = table.Column<decimal>(type: "decimal(18, 2)", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "IncidenciasSSMA",
                columns: table => new
                {
                    IncidenciaID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProyectoID = table.Column<int>(type: "INTEGER", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Severidad = table.Column<string>(type: "TEXT", nullable: false),
                    Estado = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncidenciasSSMA", x => x.IncidenciaID);
                    table.ForeignKey(
                        name: "FK_IncidenciasSSMA_Proyectos_ProyectoID",
                        column: x => x.ProyectoID,
                        principalTable: "Proyectos",
                        principalColumn: "ProyectoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemsPresupuesto",
                columns: table => new
                {
                    ItemPresupuestoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProyectoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Codigo = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Unidad = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Cantidad = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    ImporteTotal = table.Column<decimal>(type: "decimal(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsPresupuesto", x => x.ItemPresupuestoId);
                    table.ForeignKey(
                        name: "FK_ItemsPresupuesto_Proyectos_ProyectoId",
                        column: x => x.ProyectoId,
                        principalTable: "Proyectos",
                        principalColumn: "ProyectoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notificaciones",
                columns: table => new
                {
                    NotificacionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Mensaje = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Leida = table.Column<bool>(type: "INTEGER", nullable: false),
                    ProyectoId = table.Column<int>(type: "INTEGER", nullable: true),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: true),
                    RoleName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificaciones", x => x.NotificacionId);
                    table.ForeignKey(
                        name: "FK_Notificaciones_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notificaciones_Proyectos_ProyectoId",
                        column: x => x.ProyectoId,
                        principalTable: "Proyectos",
                        principalColumn: "ProyectoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OficinasTecnicas",
                columns: table => new
                {
                    DocumentoID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProyectoID = table.Column<int>(type: "INTEGER", nullable: false),
                    NombreDocumento = table.Column<string>(type: "TEXT", nullable: false),
                    RutaArchivo = table.Column<string>(type: "TEXT", nullable: false),
                    Version = table.Column<int>(type: "INTEGER", nullable: false),
                    FechaSubida = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OficinasTecnicas", x => x.DocumentoID);
                    table.ForeignKey(
                        name: "FK_OficinasTecnicas_Proyectos_ProyectoID",
                        column: x => x.ProyectoID,
                        principalTable: "Proyectos",
                        principalColumn: "ProyectoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Penalizaciones",
                columns: table => new
                {
                    PenalizacionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProyectoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Penalizaciones", x => x.PenalizacionId);
                    table.ForeignKey(
                        name: "FK_Penalizaciones_Proyectos_ProyectoId",
                        column: x => x.ProyectoId,
                        principalTable: "Proyectos",
                        principalColumn: "ProyectoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tareas",
                columns: table => new
                {
                    TareaId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProyectoId = table.Column<int>(type: "INTEGER", nullable: false),
                    UidMsProject = table.Column<int>(type: "INTEGER", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "TEXT", nullable: false),
                    WBS = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    EsResumen = table.Column<bool>(type: "INTEGER", nullable: false),
                    Unidad = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    CantidadContrato = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    ImporteContrato = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    FechaInicioReal = table.Column<DateTime>(type: "TEXT", nullable: true),
                    FechaFinReal = table.Column<DateTime>(type: "TEXT", nullable: true),
                    PorcentajeCompletadoReal = table.Column<int>(type: "INTEGER", nullable: true),
                    DuracionReal = table.Column<int>(type: "INTEGER", nullable: true),
                    EstadoAccion = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Notas = table.Column<string>(type: "TEXT", nullable: false),
                    TareaPadreId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tareas", x => x.TareaId);
                    table.ForeignKey(
                        name: "FK_Tareas_Proyectos_ProyectoId",
                        column: x => x.ProyectoId,
                        principalTable: "Proyectos",
                        principalColumn: "ProyectoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tareas_Tareas_TareaPadreId",
                        column: x => x.TareaPadreId,
                        principalTable: "Tareas",
                        principalColumn: "TareaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DependenciaTareas",
                columns: table => new
                {
                    TareaPredecesoraId = table.Column<int>(type: "INTEGER", nullable: false),
                    TareaSucesoraId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DependenciaTareas", x => new { x.TareaPredecesoraId, x.TareaSucesoraId });
                    table.ForeignKey(
                        name: "FK_DependenciaTareas_Tareas_TareaPredecesoraId",
                        column: x => x.TareaPredecesoraId,
                        principalTable: "Tareas",
                        principalColumn: "TareaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DependenciaTareas_Tareas_TareaSucesoraId",
                        column: x => x.TareaSucesoraId,
                        principalTable: "Tareas",
                        principalColumn: "TareaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EstadosDePagoItem",
                columns: table => new
                {
                    EstadoDePagoItemId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EstadoDePagoId = table.Column<int>(type: "INTEGER", nullable: false),
                    TareaId = table.Column<int>(type: "INTEGER", nullable: true),
                    ItemPresupuestoId = table.Column<int>(type: "INTEGER", nullable: true),
                    Descripcion = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Unidad = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    CantidadContrato = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    ImporteContrato = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    CantidadAvancePeriodo = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    ImporteAvancePeriodo = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    CantidadAvanceAcumulado = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    ImporteAvanceAcumulado = table.Column<decimal>(type: "decimal(18, 2)", nullable: false)
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
                        name: "FK_EstadosDePagoItem_ItemsPresupuesto_ItemPresupuestoId",
                        column: x => x.ItemPresupuestoId,
                        principalTable: "ItemsPresupuesto",
                        principalColumn: "ItemPresupuestoId");
                    table.ForeignKey(
                        name: "FK_EstadosDePagoItem_Tareas_TareaId",
                        column: x => x.TareaId,
                        principalTable: "Tareas",
                        principalColumn: "TareaId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AmpliacionesProyectos_ProyectoId",
                table: "AmpliacionesProyectos",
                column: "ProyectoId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_NumeroIdentificacion",
                table: "AspNetUsers",
                column: "NumeroIdentificacion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contratos_ProyectoID",
                table: "Contratos",
                column: "ProyectoID");

            migrationBuilder.CreateIndex(
                name: "IX_ControlesCalidad_ProyectoID",
                table: "ControlesCalidad",
                column: "ProyectoID");

            migrationBuilder.CreateIndex(
                name: "IX_DependenciaTareas_TareaSucesoraId",
                table: "DependenciaTareas",
                column: "TareaSucesoraId");

            migrationBuilder.CreateIndex(
                name: "IX_EstadosDePago_ProyectoId",
                table: "EstadosDePago",
                column: "ProyectoId");

            migrationBuilder.CreateIndex(
                name: "IX_EstadosDePagoItem_EstadoDePagoId",
                table: "EstadosDePagoItem",
                column: "EstadoDePagoId");

            migrationBuilder.CreateIndex(
                name: "IX_EstadosDePagoItem_ItemPresupuestoId",
                table: "EstadosDePagoItem",
                column: "ItemPresupuestoId");

            migrationBuilder.CreateIndex(
                name: "IX_EstadosDePagoItem_TareaId",
                table: "EstadosDePagoItem",
                column: "TareaId");

            migrationBuilder.CreateIndex(
                name: "IX_IncidenciasSSMA_ProyectoID",
                table: "IncidenciasSSMA",
                column: "ProyectoID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsPresupuesto_ProyectoId",
                table: "ItemsPresupuesto",
                column: "ProyectoId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificaciones_ProyectoId",
                table: "Notificaciones",
                column: "ProyectoId");

            migrationBuilder.CreateIndex(
                name: "IX_Notificaciones_UsuarioId",
                table: "Notificaciones",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_OficinasTecnicas_ProyectoID",
                table: "OficinasTecnicas",
                column: "ProyectoID");

            migrationBuilder.CreateIndex(
                name: "IX_Penalizaciones_ProyectoId",
                table: "Penalizaciones",
                column: "ProyectoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_ProyectoId",
                table: "Tareas",
                column: "ProyectoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_TareaPadreId",
                table: "Tareas",
                column: "TareaPadreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AmpliacionesProyectos");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Contratos");

            migrationBuilder.DropTable(
                name: "ControlesCalidad");

            migrationBuilder.DropTable(
                name: "DependenciaTareas");

            migrationBuilder.DropTable(
                name: "EstadosDePagoItem");

            migrationBuilder.DropTable(
                name: "IncidenciasSSMA");

            migrationBuilder.DropTable(
                name: "Inventarios");

            migrationBuilder.DropTable(
                name: "Notificaciones");

            migrationBuilder.DropTable(
                name: "OficinasTecnicas");

            migrationBuilder.DropTable(
                name: "Penalizaciones");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "EstadosDePago");

            migrationBuilder.DropTable(
                name: "ItemsPresupuesto");

            migrationBuilder.DropTable(
                name: "Tareas");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Proyectos");
        }
    }
}
