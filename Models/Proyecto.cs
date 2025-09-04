using System;
using System.ComponentModel.DataAnnotations;

namespace WorkFlow_SIG10._1.Models
{
    public class Proyecto
    {
        [Key]
        public int ProyectoID { get; set; }

        [Required(ErrorMessage = "El Código de Obra es requerido.")]
        public string CodigoObra { get; set; } = string.Empty;

        [Required(ErrorMessage = "El Nombre de la Obra es requerido.")]
        public string NombreObra { get; set; } = string.Empty;

        // Empresa Ejecutora
        [Required(ErrorMessage = "El Nombre de la Empresa Ejecutora es requerido.")]
        public string NombreEmpresaEjecutora { get; set; } = string.Empty;
        [Required(ErrorMessage = "El ID de la Empresa Ejecutora es requerido.")]
        public string IdEmpresaEjecutora { get; set; } = string.Empty;
        [Required(ErrorMessage = "La Dirección de la Empresa Ejecutora es requerida.")]
        public string DireccionEmpresaEjecutora { get; set; } = string.Empty;
        [Required(ErrorMessage = "La Ciudad de la Empresa Ejecutora es requerida.")]
        public string CiudadEmpresaEjecutora { get; set; } = string.Empty;
        [Required(ErrorMessage = "El Nombre del Representante Legal de la Ejecutora es requerido.")]
        public string NombreRepresentanteLegalEjecutora { get; set; } = string.Empty;
        [Required(ErrorMessage = "El ID del Representante Legal de la Ejecutora es requerido.")]
        public string IdRepresentanteLegalEjecutora { get; set; } = string.Empty;
        [Required(ErrorMessage = "El Nombre del Administrador de Obra de la Ejecutora es requerido.")]
        public string NombreAdministradorObraEjecutora { get; set; } = string.Empty;

        // Ubicación de la Obra
        [Required(ErrorMessage = "El País es requerido.")]
        public string Pais { get; set; } = string.Empty;
        [Required(ErrorMessage = "La Región es requerida.")]
        public string Region { get; set; } = string.Empty;
        [Required(ErrorMessage = "La Ciudad de la Obra es requerida.")]
        public string Ciudad { get; set; } = string.Empty;

        // Empresa Mandante
        [Required(ErrorMessage = "El Nombre de la Empresa Mandante es requerido.")]
        public string NombreEmpresaMandante { get; set; } = string.Empty;
        [Required(ErrorMessage = "El ID de la Empresa Mandante es requerido.")]
        public string IdEmpresaMandante { get; set; } = string.Empty;
        [Required(ErrorMessage = "La Dirección de la Empresa Mandante es requerida.")]
        public string DireccionEmpresaMandante { get; set; } = string.Empty;
        [Required(ErrorMessage = "La Ciudad de la Empresa Mandante es requerida.")]
        public string CiudadEmpresaMandante { get; set; } = string.Empty;
        [Required(ErrorMessage = "El Nombre del Representante Legal de la Mandante es requerido.")]
        public string NombreRepresentanteLegalMandante { get; set; } = string.Empty;
        [Required(ErrorMessage = "El ID del Representante Legal de la Mandante es requerido.")]
        public string IdRepresentanteLegalMandante { get; set; } = string.Empty;
        [Required(ErrorMessage = "El Nombre del Administrador de Obra de la Mandante es requerido.")]
        public string NombreAdministradorObraMandante { get; set; } = string.Empty;

        // Fechas del Proyecto
        [Required(ErrorMessage = "La Fecha de Inicio es requerida.")]
        public DateTime FechaInicioProyecto { get; set; } = DateTime.Today;
        [Required(ErrorMessage = "La Fecha de Término es requerida.")]
        public DateTime FechaTerminoProyecto { get; set; } = DateTime.Today;

        // Logos (as string paths for now)
        public string LogoEmpresaEjecutoraPath { get; set; } = string.Empty;
        public string LogoEmpresaMandantePath { get; set; } = string.Empty;

        [Required(ErrorMessage = "El Estado es requerido.")] // Added as it's a dropdown
        public string Estado { get; set; } = string.Empty;
    }
}