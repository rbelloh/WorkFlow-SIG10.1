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
    }
}