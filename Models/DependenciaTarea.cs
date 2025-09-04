using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkFlow_SIG10._1.Models
{
    // Esta clase representa la tabla de enlace para la relación muchos-a-muchos de las dependencias de tareas.
    public class DependenciaTarea
    {
        // La clave primaria compuesta (TareaPredecesoraId, TareaSucesoraId) se configurará
        // en ApplicationDbContext usando la API Fluida (Fluent API).

        public int TareaPredecesoraId { get; set; }
        public Tarea TareaPredecesora { get; set; }

        public int TareaSucesoraId { get; set; }
        public Tarea TareaSucesora { get; set; }
    }
}
