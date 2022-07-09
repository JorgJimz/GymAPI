namespace GymAPI.Models.Responses
{
    public class UsuarioResponse
    {
        public Header Header { get; set; }
        public Usuario UsuarioEntity { get; set; }
        public List<Usuario> UsuarioList { get; set; }
    }
}
