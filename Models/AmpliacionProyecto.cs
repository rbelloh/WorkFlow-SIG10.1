using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkFlow_SIG10._1.Models
{
    public class AmpliacionProyecto
    {
        [Key]
        public int AmpliacionId { get; set; }

        [Required]
        public int ProyectoId { get; set; }
        [ForeignKey("ProyectoId")]
        public virtual Proyecto Proyecto { get; set; } = null!;

        [Required]
        [StringLength(500)]
        public string Descripcion { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Monto { get; set; }

        [Required]
        public DateTime Fecha { get; set; }
    }
}
