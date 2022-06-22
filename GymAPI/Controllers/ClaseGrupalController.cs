using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GymAPI.Models;

namespace GymAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaseGrupalController : ControllerBase
    {
        private readonly GymContext _context;

        public ClaseGrupalController(GymContext context)
        {
            _context = context;
        }

        // GET: api/ClaseGrupal
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClaseGrupal>>> GetClasesGrupales()
        {
          if (_context.ClasesGrupales == null)
          {
              return NotFound();
          }
            return await _context.ClasesGrupales.Include(x => x.Actividad).Include(x => x.Instructor).ToListAsync();
        }

        // GET: api/ClaseGrupal/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClaseGrupal>> GetClaseGrupal(int id)
        {
          if (_context.ClasesGrupales == null)
          {
              return NotFound();
          }
            var claseGrupal = await _context.ClasesGrupales.FindAsync(id);

            if (claseGrupal == null)
            {
                return NotFound();
            }

            return claseGrupal;
        }

        // PUT: api/ClaseGrupal/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClaseGrupal(int id, ClaseGrupal claseGrupal)
        {
            if (id != claseGrupal.Id)
            {
                return BadRequest();
            }

            _context.Entry(claseGrupal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClaseGrupalExists(id))
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

        // POST: api/ClaseGrupal
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClaseGrupal>> PostClaseGrupal(ClaseGrupal claseGrupal)
        {
          if (_context.ClasesGrupales == null)
          {
              return Problem("Entity set 'GymContext.ClasesGrupales'  is null.");
          }
            _context.ClasesGrupales.Add(claseGrupal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClaseGrupal", new { id = claseGrupal.Id }, claseGrupal);
        }

        // DELETE: api/ClaseGrupal/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClaseGrupal(int id)
        {
            if (_context.ClasesGrupales == null)
            {
                return NotFound();
            }
            var claseGrupal = await _context.ClasesGrupales.FindAsync(id);
            if (claseGrupal == null)
            {
                return NotFound();
            }

            _context.ClasesGrupales.Remove(claseGrupal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClaseGrupalExists(int id)
        {
            return (_context.ClasesGrupales?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
