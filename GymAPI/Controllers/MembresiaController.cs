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
    public class MembresiaController : ControllerBase
    {
        private readonly GymContext _context;

        public MembresiaController(GymContext context)
        {
            _context = context;
        }

        // GET: api/Membresia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Membresia>>> GetMembresias()
        {
          if (_context.Membresias == null)
          {
              return NotFound();
          }
            return await _context.Membresias.ToListAsync();
        }

        // GET: api/Membresia/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Membresia>> GetMembresia(int id)
        {
          if (_context.Membresias == null)
          {
              return NotFound();
          }
            var membresia = await _context.Membresias.FindAsync(id);

            if (membresia == null)
            {
                return NotFound();
            }

            return membresia;
        }

        // PUT: api/Membresia/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMembresia(int id, Membresia membresia)
        {
            if (id != membresia.Id)
            {
                return BadRequest();
            }

            _context.Entry(membresia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MembresiaExists(id))
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

        // POST: api/Membresia
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Membresia>> PostMembresia(Membresia membresia)
        {
          if (_context.Membresias == null)
          {
              return Problem("Entity set 'GymContext.Membresias'  is null.");
          }
            _context.Membresias.Add(membresia);

            //Actualizamos Status Usuario
            Usuario u = _context.Usuarios.Find(membresia.UsuarioId);
            u.Status = 1;

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMembresia", new { id = membresia.Id }, membresia);
        }

        // DELETE: api/Membresia/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMembresia(int id)
        {
            if (_context.Membresias == null)
            {
                return NotFound();
            }
            var membresia = await _context.Membresias.FindAsync(id);
            if (membresia == null)
            {
                return NotFound();
            }

            _context.Membresias.Remove(membresia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MembresiaExists(int id)
        {
            return (_context.Membresias?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
