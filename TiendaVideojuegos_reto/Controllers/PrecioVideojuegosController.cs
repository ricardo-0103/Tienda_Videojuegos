using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaVideojuegos_reto.DBContext;
using TiendaVideojuegos_reto.Models;

namespace TiendaVideojuegos_reto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrecioVideojuegosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PrecioVideojuegosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/PrecioVideojuegos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrecioVideojuego>>> GetPrecioVideojuegos()
        {
          if (_context.PrecioVideojuegos == null)
          {
              return NotFound($"No existen videojuegos registrados.");
          }

            var precioVideojuegos = await _context.PrecioVideojuegos
                  .Include(p => p.IdJuegoNavigation).ToListAsync();

            var listaPrecios = new List<object>();
            
            foreach (var pv in precioVideojuegos)
            {
                var infoVideojuego = new
                {
                    idPrecio = pv.IdPrecio,
                    idVideojuego = pv.IdJuego,
                    nombreVideojuego = pv.IdJuegoNavigation.Nombre,
                    precioDia = pv.PrecioDia,
                    plataforma = pv.IdJuegoNavigation.Plataforma
                };
                listaPrecios.Add(infoVideojuego);
                            }

            return Ok(listaPrecios);
        }

        // GET: api/PrecioVideojuegos/5
        [HttpGet("{idJuego}")]
        public async Task<ActionResult<PrecioVideojuego>> GetPrecioVideojuego(int idJuego)
        {
          if (_context.PrecioVideojuegos == null)
          {
              return NotFound();
          }

            var pv = await _context.PrecioVideojuegos
                  .Include(p => p.IdJuegoNavigation)
                  .SingleOrDefaultAsync(p => p.IdJuego == idJuego);

            if (pv == null)
            {
                return NotFound($"No se ha establecido precio al videojuego con id {idJuego}.");
            }

            var infoVideojuego = new
            {
                idPrecio = pv.IdPrecio,
                idVideojuego = pv.IdJuego,
                nombreVideojuego = pv.IdJuegoNavigation.Nombre,
                precioPorDia = pv.PrecioDia,
                plataforma = pv.IdJuegoNavigation.Plataforma
            };

            return Ok(infoVideojuego);
        }

        // PUT: api/PrecioVideojuegos/Actualizar
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Actualizar")]
        public async Task<IActionResult> PutPrecioVideojuego([FromBody] PrecioVideojuego precioVideojuego)
        {
            if (precioVideojuego == null || precioVideojuego.IdJuego <= 0)
            {
                return BadRequest();
            }
            _context.Entry(precioVideojuego).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrecioVideojuegoExists(precioVideojuego.IdJuego))
                {
                    return NotFound($"No existe el videojuego con el id {precioVideojuego.IdJuego}.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PrecioVideojuegos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PrecioVideojuego>> PostPrecioVideojuego([FromBody] PrecioVideojuego precioVideojuego)
        {
          if (_context.PrecioVideojuegos == null)
          {
              return Problem("Entity set 'AppDbContext.PrecioVideojuegos'  is null.");
          }
            _context.PrecioVideojuegos.Add(precioVideojuego);
            await _context.SaveChangesAsync();

            return Ok("Se asignó el precio correctamente");
        }

        // DELETE: api/PrecioVideojuegos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrecioVideojuego(int id)
        {
            if (_context.PrecioVideojuegos == null)
            {
                return NotFound();
            }
            var precioVideojuego = await _context.PrecioVideojuegos.FindAsync(id);
            if (precioVideojuego == null)
            {
                return NotFound();
            }

            _context.PrecioVideojuegos.Remove(precioVideojuego);
            await _context.SaveChangesAsync();

            return Ok("Actualizado correctamente");
        }

        private bool PrecioVideojuegoExists(int id)
        {
            return (_context.PrecioVideojuegos?.Any(e => e.IdPrecio == id)).GetValueOrDefault();
        }
    }
}
