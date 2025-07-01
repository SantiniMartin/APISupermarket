using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComparadorPreciosAPI.Data;
using ComparadorPreciosAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace ComparadorPreciosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarritosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CarritosController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Carrito>>> GetAll() => await _context.Carritos.Include(c => c.Items).ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Carrito>> GetById(int id)
        {
            var carrito = await _context.Carritos.Include(c => c.Items).FirstOrDefaultAsync(c => c.Id == id);
            if (carrito == null) return NotFound();
            return carrito;
        }

        [HttpPost]
        public async Task<ActionResult<Carrito>> Create(Carrito carrito)
        {
            _context.Carritos.Add(carrito);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = carrito.Id }, carrito);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Carrito carrito)
        {
            if (id != carrito.Id) return BadRequest();
            _context.Entry(carrito).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException) { if (!_context.Carritos.Any(e => e.Id == id)) return NotFound(); else throw; }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var carrito = await _context.Carritos.FindAsync(id);
            if (carrito == null) return NotFound();
            _context.Carritos.Remove(carrito);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
} 