using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkFlow_SIG10._1.Models
{
    public class EstadoDePagoItem
    {
        [Key]
        public int EstadoDePagoItemId { get; set; }

        [Required]
        public int EstadoDePagoId { get; set; }
        [ForeignKey("EstadoDePagoId")]
        public virtual EstadoDePago EstadoDePago { get; set; } = null!;

        public int? TareaId { get; set; }
        [ForeignKey("TareaId")]
        public virtual Tarea Tarea { get; set; } = null!;

        public int? ItemPresupuestoId { get; set; }
        [ForeignKey("ItemPresupuestoId")]
        public virtual ItemPresupuesto ItemPresupuesto { get; set; } = null!;

        // --- Data for the item (either from Tarea, ItemPresupuesto, or custom) ---
        [Required]
        [StringLength(255)]
        public string Descripcion { get; set; } = string.Empty; // Can be from Tarea.Nombre, ItemPresupuesto.Descripcion, or custom

        [StringLength(50)]
        public string Unidad { get; set; } = string.Empty; // Can be from Tarea.Unidad, ItemPresupuesto.Unidad, or custom

        [Column(TypeName = "decimal(18, 2)")]
        public decimal PrecioUnitario { get; set; } // Can be from Tarea.PrecioUnitario, ItemPresupuesto.PrecioUnitario, or custom

        [Column(TypeName = "decimal(18, 2)")]
        public decimal CantidadContrato { get; set; } // Can be from Tarea.CantidadContrato, ItemPresupuesto.Cantidad, or custom

        [Column(TypeName = "decimal(18, 2)")]
        public decimal ImporteContrato { get; set; } // Calculated: CantidadContrato * PrecioUnitario, or from ItemPresupuesto.ImporteTotal

        // --- Progress for THIS EP ---
        [Column(TypeName = "decimal(18, 2)")]
        public decimal CantidadAvancePeriodo { get; set; } // User input

        // --- Calculated values for THIS EP ---
        [Column(TypeName = "decimal(18, 2)")]
        public decimal ImporteAvancePeriodo { get; set; } // CantidadAvancePeriodo * PrecioUnitario

        // --- Accumulated values from previous EPs + this EP ---
        [Column(TypeName = "decimal(18, 2)")]
        public decimal CantidadAvanceAcumulado { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal ImporteAvanceAcumulado { get; set; }
    }
}