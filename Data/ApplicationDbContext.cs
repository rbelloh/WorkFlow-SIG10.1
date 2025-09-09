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
        public DbSet<Notificacion> Notificaciones { get; set; }
        public DbSet<AmpliacionProyecto> AmpliacionesProyectos { get; set; }
        public DbSet<Penalizacion> Penalizaciones { get; set; }
        public DbSet<EstadoDePago> EstadosDePago { get; set; }
        public DbSet<EstadoDePagoItem> EstadosDePagoItem { get; set; }
        public DbSet<ItemPresupuesto> ItemsPresupuesto { get; set; }

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
                .OnDelete(DeleteBehavior.Cascade); // Permite la eliminación en cascada de subtareas

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

            // --- Configure cascade delete for Proyecto and related entities ---
            builder.Entity<Proyecto>()
                .HasMany(p => p.Tareas)
                .WithOne(t => t.Proyecto)
                .HasForeignKey(t => t.ProyectoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Proyecto>()
                .HasMany(p => p.AmpliacionesProyectos)
                .WithOne(ac => ac.Proyecto)
                .HasForeignKey(ac => ac.ProyectoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Proyecto>()
                .HasMany(p => p.Penalizaciones)
                .WithOne(pe => pe.Proyecto)
                .HasForeignKey(pe => pe.ProyectoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Proyecto>()
                .HasMany(p => p.EstadosDePago)
                .WithOne(ep => ep.Proyecto)
                .HasForeignKey(ep => ep.ProyectoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Proyecto>()
                .HasMany(p => p.ItemsPresupuesto)
                .WithOne(ip => ip.Proyecto)
                .HasForeignKey(ip => ip.ProyectoId)
                .OnDelete(DeleteBehavior.Cascade);

            // For Notificacion, ProyectoId is nullable, so Cascade will delete if linked, otherwise set to null if not linked.
            // If ProyectoId is required, Cascade is fine. If it's nullable, ClientSetNull is default, Cascade will override.
            builder.Entity<Proyecto>()
                .HasMany(p => p.Notificaciones)
                .WithOne(n => n.Proyecto)
                .HasForeignKey(n => n.ProyectoId)
                .OnDelete(DeleteBehavior.Cascade); // Or ClientSetNull if you want to keep notifications but unlink them
        }
    }
}