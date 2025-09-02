using System;
using System.ComponentModel.DataAnnotations;

namespace WorkFlow_SIG10._1.Models
{
    public class Proyecto
    {
        [Key]
        public int ProyectoID { get; set; }
        public string NombreProyecto { get; set; } = string.Empty;
        public string Ubicacion { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinEstimada { get; set; }
        public decimal PresupuestoTotal { get; set; }
        public string Estado { get; set; } = string.Empty;
    }
}