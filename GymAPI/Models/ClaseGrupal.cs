using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymAPI.Models
{
    public class ClaseGrupal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime FechaHora { get; set; }
        public int InstructorId { get; set; }
        public Usuario? Instructor { get; set; }
        public int ActividadId { get; set; }
        public Actividad? Actividad { get; set; }
        public int Status { get; set; }

    }
}
