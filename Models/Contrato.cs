using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkFlow_SIG10._1.Models
{
    public class Contrato
    {
        [Key]
        public int ContratoID { get; set; }
        
        [ForeignKey("Proyecto")]
        public int ProyectoID { get; set; }
        public Proyecto? Proyecto { get; set; }
        
        public string TipoContrato { get; set; } = string.Empty;
        public decimal ValorContrato { get; set; }
        public DateTime FechaFirma { get; set; }
        public string Estado { get; set; } = string.Empty;
    }
}