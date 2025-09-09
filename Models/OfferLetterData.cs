using System;
using System.ComponentModel.DataAnnotations;

namespace WorkFlow_SIG10._1.Models
{
    public class OfferLetterData
    {
        [Required(ErrorMessage = "El nombre del destinatario es requerido.")]
        public string RecipientName { get; set; } = string.Empty;

        [Required(ErrorMessage = "La obra es requerida.")]
        public string ProjectName { get; set; } = string.Empty;

        [Required(ErrorMessage = "La ciudad es requerida.")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "La comuna es requerida.")]
        public string Commune { get; set; } = string.Empty;

        [Required(ErrorMessage = "La región es requerida.")]
        public string Region { get; set; } = string.Empty;

        [Required(ErrorMessage = "El país es requerido.")]
        public string Country { get; set; } = string.Empty;

        [Required(ErrorMessage = "El cargo es requerido.")]
        public string Position { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de inicio es requerida.")]
        public DateTime StartDate { get; set; } = DateTime.Today;

        public DateTime? EndDate { get; set; }

        public bool IsIndefinite { get; set; }

        [Required(ErrorMessage = "El sueldo líquido es requerido.")]
        [Range(0, double.MaxValue, ErrorMessage = "El sueldo debe ser un valor positivo.")]
        public decimal NetSalary { get; set; }

        [Required(ErrorMessage = "El turno o jornada laboral es requerido.")]
        public string WorkSchedule { get; set; } = string.Empty;

        [Required(ErrorMessage = "Las condiciones generales son requeridas.")]
        public string GeneralConditions { get; set; } = string.Empty;

        [Required(ErrorMessage = "Los requisitos de contratación son requeridos.")]
        public string HiringRequirements { get; set; } = string.Empty;
    }
}