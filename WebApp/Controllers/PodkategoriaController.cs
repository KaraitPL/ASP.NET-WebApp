using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PodkategoriaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PodkategoriaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Podkategoria
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Podkategoria>>> GetPodkategoria()
        {
          if (_context.Podkategoria == null)
          {
              return NotFound();
          }
            return await _context.Podkategoria.ToListAsync();
        }

        // GET: api/Podkategoria/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Podkategoria>> GetPodkategoria(int id)
        {
          if (_context.Podkategoria == null)
          {
              return NotFound();
          }
            var podkategoria = await _context.Podkategoria.FindAsync(id);

            if (podkategoria == null)
            {
                return NotFound();
            }

            return podkategoria;
        }

        // PUT: api/Podkategoria/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPodkategoria(int id, Podkategoria podkategoria)
        {
            if (id != podkategoria.Id)
            {
                return BadRequest();
            }

            _context.Entry(podkategoria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PodkategoriaExists(id))
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

        // POST: api/Podkategoria
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Podkategoria>> PostPodkategoria(Podkategoria podkategoria)
        {
          if (_context.Podkategoria == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Podkategoria'  is null.");
          }
            _context.Podkategoria.Add(podkategoria);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPodkategoria", new { id = podkategoria.Id }, podkategoria);
        }

        // DELETE: api/Podkategoria/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePodkategoria(int id)
        {
            if (_context.Podkategoria == null)
            {
                return NotFound();
            }
            var podkategoria = await _context.Podkategoria.FindAsync(id);
            if (podkategoria == null)
            {
                return NotFound();
            }

            _context.Podkategoria.Remove(podkategoria);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PodkategoriaExists(int id)
        {
            return (_context.Podkategoria?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
