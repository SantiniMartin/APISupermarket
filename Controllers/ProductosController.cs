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
    public class ProductosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProductosController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetAll() => await _context.Productos.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetById(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) return NotFound();
            return producto;
        }

        [HttpPost]
        public async Task<ActionResult<Producto>> Create(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = producto.Id }, producto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Producto producto)
        {
            if (id != producto.Id) return BadRequest();
            _context.Entry(producto).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException) { if (!_context.Productos.Any(e => e.Id == id)) return NotFound(); else throw; }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) return NotFound();
            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Endpoint personalizado: productos por supermercado
        [HttpGet("supermercado/{supermercadoId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetProductosPorSupermercado(int supermercadoId)
        {
            var productos = await _context.ProductoSupermercados
                .Where(ps => ps.SupermercadoId == supermercadoId)
                .Include(ps => ps.Producto)
                    .ThenInclude(p => p.Categoria)
                .Select(ps => new
                {
                    ps.ProductoId,
                    ps.Producto.Nombre,
                    ps.Producto.Imagen,
                    ps.Precio,
                    ps.Producto.UnidadMedida,
                    Categoria = ps.Producto.Categoria.Nombre
                })
                .ToListAsync();

            return Ok(productos);
        }
    }
}
