using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkFlow_SIG10._1.Models
{
    public class Tarea
    {
        [Key]
        public int TareaId { get; set; }

        [Required]
        public int ProyectoId { get; set; }
        [ForeignKey("ProyectoId")]
        public Proyecto Proyecto { get; set; } = null!;

        // --- Datos Proyectados (desde la importación) ---
        [Required]
        public int UidMsProject { get; set; }

        [Required]
        [StringLength(255)]
        public string Nombre { get; set; } = string.Empty;

        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        [StringLength(50)]
        public string WBS { get; set; } = string.Empty;

        public bool EsResumen { get; set; }

        [StringLength(50)]
        public string Unidad { get; set; } = string.Empty; // e.g., "m2", "und", "kg"

        [Column(TypeName = "decimal(18, 2)")]
        public decimal CantidadContrato { get; set; } // Quantity from contract/BOQ

        [Column(TypeName = "decimal(18, 2)")]
        public decimal PrecioUnitario { get; set; } // Unit price from contract/BOQ

        [Column(TypeName = "decimal(18, 2)")]
        public decimal ImporteContrato { get; set; } // Calculated: CantidadContrato * PrecioUnitario

        // --- Datos Reales (ingresados por el usuario) ---
        public DateTime? FechaInicioReal { get; set; }
        public DateTime? FechaFinReal { get; set; }
        public int? PorcentajeCompletadoReal { get; set; }
        public int? DuracionReal { get; set; } // Duración real en días

        public EstadoAccionTarea EstadoAccion { get; set; } = EstadoAccionTarea.PorEjecutar;
        public string Notas { get; set; } = string.Empty;

        // --- Relaciones ---
        public int? TareaPadreId { get; set; }
        [ForeignKey("TareaPadreId")]
        public Tarea TareaPadre { get; set; } = null!;
        public ICollection<Tarea> Subtareas { get; set; } = new List<Tarea>();

        public ICollection<DependenciaTarea> Predecesoras { get; set; } = new List<DependenciaTarea>();
        public ICollection<DependenciaTarea> Sucesoras { get; set; } = new List<DependenciaTarea>();

        public Tarea ShallowCopy()
        {
            var clone = (Tarea)this.MemberwiseClone();
            clone.Subtareas = new List<Tarea>(); // Ensure the clone has a new list for its children
            return clone;
        }
    }
}