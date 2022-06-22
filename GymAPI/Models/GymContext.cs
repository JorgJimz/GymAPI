using Microsoft.EntityFrameworkCore;

namespace GymAPI.Models
{
    public class GymContext: DbContext
    {
        public GymContext(DbContextOptions options)
           : base(options)
        {
        }
        public DbSet<Actividad> Actividades { get; set; }
        public DbSet<Asistencia> Asistencias { get; set; }
        public DbSet<ClaseGrupal> ClasesGrupales { get; set; }
        public DbSet<Inscripcion> Inscripciones { get; set; }
        public DbSet<Membresia> Membresias { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
