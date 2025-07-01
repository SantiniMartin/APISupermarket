using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComparadorPreciosAPI.Data;
using ComparadorPreciosAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComparadorPreciosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarcasController : ControllerBase
    {
        private readonly AppDbContext _context;
        public MarcasController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Marca>>> GetAll() => await _context.Marcas.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Marca>> GetById(int id)
        {
            var marca = await _context.Marcas.FindAsync(id);
            if (marca == null) return NotFound();
            return marca;
        }

        [HttpPost]
        public async Task<ActionResult<Marca>> Create(Marca marca)
        {
            _context.Marcas.Add(marca);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = marca.Id }, marca);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Marca marca)
        {
            if (id != marca.Id) return BadRequest();
            _context.Entry(marca).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException) { if (!_context.Marcas.Any(e => e.Id == id)) return NotFound(); else throw; }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var marca = await _context.Marcas.FindAsync(id);
            if (marca == null) return NotFound();
            _context.Marcas.Remove(marca);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
} 