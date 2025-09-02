using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkFlow_SIG10._1.Models
{
    public class ControlCalidad
    {
        [Key]
        public int ControlID { get; set; }
        
        [ForeignKey("Proyecto")]
        public int ProyectoID { get; set; }
        public Proyecto? Proyecto { get; set; }
        
        public string DescripcionInspeccion { get; set; } = string.Empty;
        public string Resultado { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
    }
}