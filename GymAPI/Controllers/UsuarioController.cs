using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GymAPI.Models;
using GymAPI.Models.Responses;
using GymAPI.Enums;

namespace GymAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly GymContext _context;

        public UsuarioController(GymContext context)
        {
            _context = context;
        }

        // GET: api/Usuario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            if (_context.Usuarios == null)
            {
                return NotFound();
            }
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/Usuario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            if (_context.Usuarios == null)
            {
                return NotFound();
            }
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // PUT: api/Usuario/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Usuario
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'GymContext.Usuarios'  is null.");
            }
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = usuario.Id }, usuario);
        }

        // DELETE: api/Usuario/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            if (_context.Usuarios == null)
            {
                return NotFound();
            }
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(int id)
        {
            return (_context.Usuarios?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        #region Custom Methods
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(string usuario, string contrasena)
        {
            UsuarioResponse response = new UsuarioResponse();
            Header header = new Header();
            Usuario? logged = await _context.Usuarios
                .Where(x => x.NumeroDocumento.Equals(usuario)
            && x.Contrasena.Equals(contrasena)).FirstOrDefaultAsync();
            if (logged is null)
            {
                header.Codigo = HeaderEnum.Correcto.ToString();
                header.Descripcion = "Usuario y/o Contraseña Incorrectos.";                
            }
            else
            {
                if (logged.Status == 0)
                {
                    header.Codigo = HeaderEnum.Correcto.ToString();
                    header.Descripcion = "No Cuenta con Membresía Activa.";                    
                }
                else
                {
                    header.Codigo = HeaderEnum.Correcto.ToString();
                    header.Descripcion = string.Empty;
                    response.UsuarioEntity = logged;
                }
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("ByDocumentNumber")]
        public async Task<IActionResult> GetUsuarioByDocumentNumber(string numero)
        {
            Usuario logged = await _context.Usuarios.Where(x => x.NumeroDocumento.Equals(numero)).FirstOrDefaultAsync();
            return Ok(logged);
        }

        [HttpGet]
        [Route("GetIntructors")]
        public async Task<IActionResult> GetInstructors()
        {
            var lInstructors = await _context.Usuarios.Where(x => x.Perfil == 3).ToListAsync();
            return Ok(lInstructors);
        }
        #endregion

    }
}
