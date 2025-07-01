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
    public class SupermercadosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public SupermercadosController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supermercado>>> GetAll() => await _context.Supermercados.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Supermercado>> GetById(int id)
        {
            var supermercado = await _context.Supermercados.FindAsync(id);
            if (supermercado == null) return NotFound();
            return supermercado;
        }

        [HttpPost]
        public async Task<ActionResult<Supermercado>> Create(Supermercado supermercado)
        {
            _context.Supermercados.Add(supermercado);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = supermercado.Id }, supermercado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Supermercado supermercado)
        {
            if (id != supermercado.Id) return BadRequest();
            _context.Entry(supermercado).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException) { if (!_context.Supermercados.Any(e => e.Id == id)) return NotFound(); else throw; }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var supermercado = await _context.Supermercados.FindAsync(id);
            if (supermercado == null) return NotFound();
            _context.Supermercados.Remove(supermercado);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
