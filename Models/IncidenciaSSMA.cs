using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkFlow_SIG10._1.Models
{
    public class IncidenciaSSMA
    {
        [Key]
        public int IncidenciaID { get; set; }
        
        [ForeignKey("Proyecto")]
        public int ProyectoID { get; set; }
        public Proyecto? Proyecto { get; set; }
        
        public string Descripcion { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public string Severidad { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
    }
}