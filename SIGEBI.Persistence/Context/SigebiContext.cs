using Microsoft.EntityFrameworkCore;
using SIGEBI.Domain.Entities;

namespace SIGEBI.Persistence.Context
{
    public sealed class SigebiContext : DbContext
    {
        public SigebiContext(DbContextOptions<SigebiContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<RecursoBibliografico> RecursosBibliograficos { get; set; }
        public DbSet<Ejemplar> Ejemplares { get; set; }
        public DbSet<Prestamo> Prestamos { get; set; }
        public DbSet<Devolucion> Devoluciones { get; set; }
        public DbSet<Penalizacion> Penalizaciones { get; set; }
        public DbSet<Notificacion> Notificaciones { get; set; }
        public DbSet<Auditoria> Auditorias { get; set; }
    }
}
