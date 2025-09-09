using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkFlow_SIG10._1.Models
{
    public class EstadoDePago
    {
        [Key]
        public int EstadoDePagoId { get; set; }

        [Required]
        public int ProyectoId { get; set; }
        [ForeignKey("ProyectoId")]
        public virtual Proyecto Proyecto { get; set; } = null!;

        [Required]
        public int NumeroEP { get; set; } // e.g., 1, 2, 3...

        [Required]
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        [Required]
        [StringLength(50)]
        public string Periodo { get; set; } = string.Empty; // e.g., "Enero 2025"

        [Required]
        [StringLength(50)]
        public string Estado { get; set; } = "Borrador"; // Borrador, Enviado, Aprobado, Rechazado

        // --- Carátula Summary Values (from Excel) ---
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalContratoOriginalNeto { get; set; } // T1

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalAmpliacionesNeto { get; set; } // T2

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalPenalizacionesNeto { get; set; } // T3

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalContratoActualizadoNeto { get; set; } // T4 = T1 + T2 - T3

        [Column(TypeName = "decimal(18, 2)")]
        public decimal AvancePeriodoNeto { get; set; } // Sum of all item advances for this period

        [Column(TypeName = "decimal(18, 2)")]
        public decimal RetencionPeriodoNeto { get; set; } // A1 (e.g., 5% of AvancePeriodoNeto)

        [Column(TypeName = "decimal(18, 2)")]
        public decimal ImpuestoIvaRetencion { get; set; } // A2

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalImporteFacturacionAvanceMensual { get; set; } // A4 (AvancePeriodoNeto - RetencionPeriodoNeto)

        // --- Historical/Accumulated Values (from Resumen certificaciones) ---
        [Column(TypeName = "decimal(18, 2)")]
        public decimal RetencionesAcumuladasNeto { get; set; } // T5

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalImporteAcumuladoNeto { get; set; } // A7

        public virtual ICollection<EstadoDePagoItem> Items { get; set; } = new List<EstadoDePagoItem>();
    }
}
