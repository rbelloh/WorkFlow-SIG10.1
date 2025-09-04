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
        public Proyecto Proyecto { get; set; }

        // --- Datos Proyectados (desde la importación) ---
        [Required]
        public int UidMsProject { get; set; }

        [Required]
        [StringLength(255)]
        public string Nombre { get; set; }

        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        [StringLength(50)]
        public string WBS { get; set; }

        public bool EsResumen { get; set; }

        // --- Datos Reales (ingresados por el usuario) ---
        public DateTime? FechaInicioReal { get; set; }
        public DateTime? FechaFinReal { get; set; }
        public int? PorcentajeCompletadoReal { get; set; }
        public string Notas { get; set; }

        // --- Relaciones ---
        public int? TareaPadreId { get; set; }
        [ForeignKey("TareaPadreId")]
        public Tarea TareaPadre { get; set; }
        public ICollection<Tarea> Subtareas { get; set; } = new List<Tarea>();

        public ICollection<DependenciaTarea> Predecesoras { get; set; } = new List<DependenciaTarea>();
        public ICollection<DependenciaTarea> Sucesoras { get; set; } = new List<DependenciaTarea>();
    }
}
