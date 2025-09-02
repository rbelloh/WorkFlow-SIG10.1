using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkFlow_SIG10._1.Models
{
    public class OficinaTecnica
    {
        [Key]
        public int DocumentoID { get; set; }
        
        [ForeignKey("Proyecto")]
        public int ProyectoID { get; set; }
        public Proyecto? Proyecto { get; set; }

        public string NombreDocumento { get; set; } = string.Empty;
        public string RutaArchivo { get; set; } = string.Empty;
        public int Version { get; set; }
        public DateTime FechaSubida { get; set; }
    }
}