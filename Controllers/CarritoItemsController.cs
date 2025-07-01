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
    public class CarritoItemsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CarritoItemsController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarritoItem>>> GetAll() => await _context.CarritoItems.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<CarritoItem>> GetById(int id)
        {
            var item = await _context.CarritoItems.FindAsync(id);
            if (item == null) return NotFound();
            return item;
        }

        [HttpPost]
        public async Task<ActionResult<CarritoItem>> Create(CarritoItem item)
        {
            _context.CarritoItems.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CarritoItem item)
        {
            if (id != item.Id) return BadRequest();
            _context.Entry(item).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException) { if (!_context.CarritoItems.Any(e => e.Id == id)) return NotFound(); else throw; }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.CarritoItems.FindAsync(id);
            if (item == null) return NotFound();
            _context.CarritoItems.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
} 