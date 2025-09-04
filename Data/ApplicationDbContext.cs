using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkFlow_SIG10._1.Models;

namespace WorkFlow_SIG10._1.Data
{
    public class ApplicationDbContext : IdentityDbContext<Usuario, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<Contrato> Contratos { get; set; }
        public DbSet<Inventario> Inventarios { get; set; }
        public DbSet<IncidenciaSSMA> IncidenciasSSMA { get; set; }
        public DbSet<ControlCalidad> ControlesCalidad { get; set; }
        public DbSet<OficinaTecnica> OficinasTecnicas { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<DependenciaTarea> DependenciaTareas { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            // Configura el índice único para NumeroIdentificacion en Usuario
            builder.Entity<Usuario>().HasIndex(u => u.NumeroIdentificacion).IsUnique();

            // Configura la relación de auto-referencia de Tarea para la jerarquía padre-hijo
            builder.Entity<Tarea>()
                .HasOne(t => t.TareaPadre)
                .WithMany(t => t.Subtareas)
                .HasForeignKey(t => t.TareaPadreId)
                .OnDelete(DeleteBehavior.Restrict); // Evita problemas de eliminación en cascada

            // Configura la clave primaria compuesta para DependenciaTarea
            builder.Entity<DependenciaTarea>()
                .HasKey(dt => new { dt.TareaPredecesoraId, dt.TareaSucesoraId });

            // Configura la relación muchos-a-muchos para las Predecesoras
            builder.Entity<DependenciaTarea>()
                .HasOne(dt => dt.TareaSucesora)
                .WithMany(t => t.Predecesoras)
                .HasForeignKey(dt => dt.TareaSucesoraId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configura la relación muchos-a-muchos para las Sucesoras
            builder.Entity<DependenciaTarea>()
                .HasOne(dt => dt.TareaPredecesora)
                .WithMany(t => t.Sucesoras)
                .HasForeignKey(dt => dt.TareaPredecesoraId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
