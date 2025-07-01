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
    public class ProductoSupermercadosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProductoSupermercadosController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoSupermercado>>> GetAll() => await _context.ProductoSupermercados.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoSupermercado>> GetById(int id)
        {
            var ps = await _context.ProductoSupermercados.FindAsync(id);
            if (ps == null) return NotFound();
            return ps;
        }

        [HttpPost]
        public async Task<ActionResult<ProductoSupermercado>> Create(ProductoSupermercado ps)
        {
            _context.ProductoSupermercados.Add(ps);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = ps.Id }, ps);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProductoSupermercado ps)
        {
            if (id != ps.Id) return BadRequest();
            _context.Entry(ps).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException) { if (!_context.ProductoSupermercados.Any(e => e.Id == id)) return NotFound(); else throw; }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ps = await _context.ProductoSupermercados.FindAsync(id);
            if (ps == null) return NotFound();
            _context.ProductoSupermercados.Remove(ps);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
} 