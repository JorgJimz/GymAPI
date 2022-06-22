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
    public class AsistenciaController : ControllerBase
    {
        private readonly GymContext _context;

        public AsistenciaController(GymContext context)
        {
            _context = context;
        }

        // GET: api/Asistencia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asistencia>>> GetAsistencias()
        {
          if (_context.Asistencias == null)
          {
              return NotFound();
          }
            return await _context.Asistencias.ToListAsync();
        }

        // GET: api/Asistencia/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Asistencia>> GetAsistencia(int id)
        {
          if (_context.Asistencias == null)
          {
              return NotFound();
          }
            var asistencia = await _context.Asistencias.FindAsync(id);

            if (asistencia == null)
            {
                return NotFound();
            }

            return asistencia;
        }

        // PUT: api/Asistencia/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsistencia(int id, Asistencia asistencia)
        {
            if (id != asistencia.Id)
            {
                return BadRequest();
            }

            _context.Entry(asistencia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AsistenciaExists(id))
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

        // POST: api/Asistencia
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Asistencia>> PostAsistencia(Asistencia asistencia)
        {
          if (_context.Asistencias == null)
          {
              return Problem("Entity set 'GymContext.Asistencias'  is null.");
          }
            _context.Asistencias.Add(asistencia);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAsistencia", new { id = asistencia.Id }, asistencia);
        }

        // DELETE: api/Asistencia/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsistencia(int id)
        {
            if (_context.Asistencias == null)
            {
                return NotFound();
            }
            var asistencia = await _context.Asistencias.FindAsync(id);
            if (asistencia == null)
            {
                return NotFound();
            }

            _context.Asistencias.Remove(asistencia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AsistenciaExists(int id)
        {
            return (_context.Asistencias?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
