using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymAPI.Models
{
    public class Inscripcion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ClaseGrupalId { get; set; }        
        public ClaseGrupal? ClaseGrupal { get; set; }
        public int UsuarioId { get; set; }        
        public Usuario? Usuario { get; set; }

    }
}
