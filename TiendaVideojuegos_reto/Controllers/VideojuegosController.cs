using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
    public class VideojuegosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VideojuegosController(AppDbContext context)
        {
            _context = context;
        }

        //Listado de Videojuegos
        // GET: api/Videojuegos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Videojuego>>> GetVideojuegos()
        {
          if (_context.Videojuegos == null)
          {
              return NotFound($"No existen videojuegos registrados.");
          }

          var videojuegos = await _context.Videojuegos.Include(v => v.PrecioVideojuegos).ToListAsync();

            var listaVideojuegos = new List<object>();

            foreach (var juego in videojuegos)
            {
                var infoVideojuego = new
                {
                    idjuego = juego.IdJuego,
                    nombre = juego.Nombre,
                    año = juego.Año,
                    protagonistas = juego.Protagonistas,
                    productor = juego.Productor,
                    director = juego.Director,
                    plataforma = juego.Plataforma,
                    precioDia = juego.PrecioVideojuegos.LastOrDefault()?.PrecioDia != null
                    ? juego.PrecioVideojuegos.LastOrDefault().PrecioDia.ToString()
                    : "No está definido"
                };

                listaVideojuegos.Add(infoVideojuego);
            }

            return Ok(listaVideojuegos);

        }

        //Listado de Juegos por director de juego
        // GET: api/Videojuegos/Director/Sefton Hill
        [HttpGet("Director/{nombreDirector}")]
        public async Task<ActionResult<Videojuego>> GetDirector(string nombreDirector)
        {
            if (_context.Videojuegos == null)
            {
                return NotFound();
            }
            var videojuegos = await _context.Videojuegos.Where(v => v.Director == nombreDirector).ToListAsync();

            if (videojuegos == null || videojuegos.Count == 0)
            {
                return NotFound($"No existen videojuegos registrados con el director {nombreDirector}.");
            }

            return Ok(videojuegos);
        }

        //Listado de Juegos por protagonistas del juego
        // GET: api/Videojuegos/Protagonistas/Batman
        [HttpGet("Protagonistas/{nombresProtagonistas}")]
        public async Task<ActionResult<Videojuego>> GetProtagonistas(string nombresProtagonistas)
        {
            if (_context.Videojuegos == null)
            {
                return NotFound();
            }

            var videojuegos = await _context.Videojuegos.Where(v => v.Protagonistas == nombresProtagonistas).ToListAsync();

            if (videojuegos == null || videojuegos.Count == 0)
            {
                return NotFound($"No existen videojuegos registrados con los protagonistas {nombresProtagonistas}.");
            }

            return Ok(videojuegos);
        }

        //Listado de Juegos por productor del juego
        // GET: api/Videojuegos/Productor/Warner Bros
        [HttpGet("Productor/{nombreProductor}")]
        public async Task<ActionResult<Videojuego>> GetProductor(string nombreProductor)
        {
            if (_context.Videojuegos == null)
            {
                return NotFound();
            }
            var videojuegos = await _context.Videojuegos.Where(v => v.Productor == nombreProductor).ToListAsync();

            if (videojuegos == null || videojuegos.Count == 0)
            {
                return NotFound($"No existen videojuegos registrados con el productor {nombreProductor}.");
            }

            return Ok(videojuegos);
        }

        //Listado de Juegos por fecha de lanzamiento
        // GET: api/Videojuegos/Fecha/2011
        [HttpGet("Fecha/{año}")]
        public async Task<ActionResult<Videojuego>> GetAño(int año)
        {
            if (_context.Videojuegos == null)
            {
                return NotFound();
            }
            var videojuegos = await _context.Videojuegos.Where(v => v.Año == año).ToListAsync();

            if (videojuegos == null || videojuegos.Count == 0)
            {
                return NotFound($"No existen videojuegos registrados que hayan sido lanzados en el año {año}.");
            }

            return Ok(videojuegos);
        }

        // GET: api/Videojuegos/5
        [HttpGet("{idJuego}")]
        public async Task<ActionResult<Videojuego>> GetVideojuego(int idJuego)
        {
          if (_context.Videojuegos == null)
          {
              return NotFound();
          }

            var juego = await _context.Videojuegos
                .Include(v => v.PrecioVideojuegos)
                .SingleOrDefaultAsync(v => v.IdJuego == idJuego);

            if (juego == null)
            {
                return NotFound($"No existen videojuegos registrado con el id {idJuego}.");
            }

            var infoJuego = new
            {
                idjuego = juego.IdJuego,
                nombre = juego.Nombre,
                año = juego.Año,
                protagonistas = juego.Protagonistas,
                productor = juego.Productor,
                director = juego.Director,
                plataforma = juego.Plataforma,
                precioDia = juego.PrecioVideojuegos.LastOrDefault()?.PrecioDia != null
                ? juego.PrecioVideojuegos.LastOrDefault().PrecioDia.ToString()
                : "No está definido"
            };
            return Ok(infoJuego);
        }

        // PUT: api/Videojuegos/Actualizar
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Actualizar")]
        public async Task<IActionResult> PutVideojuego([FromBody] Videojuego videojuego)
        {
            if (videojuego == null || videojuego.IdJuego <= 0)
            {
                return BadRequest();
            }

            _context.Entry(videojuego).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VideojuegoExists(videojuego.IdJuego))
                {
                    return NotFound($"No se encontró el videojuego con el id {videojuego.IdJuego}.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Videojuegos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Videojuego>> PostVideojuego([FromBody] Videojuego videojuego)
        {
          if (_context.Videojuegos == null)
          {
              return Problem("Entity set 'AppDbContext.Videojuegos'  is null.");
          }
            _context.Videojuegos.Add(videojuego);
            await _context.SaveChangesAsync();

            return Ok($"Precio establecido con éxito");
        }

        // DELETE: api/Videojuegos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVideojuego(int id)
        {
            if (_context.Videojuegos == null)
            {
                return NotFound();
            }
            var videojuego = await _context.Videojuegos.FindAsync(id);
            if (videojuego == null)
            {
                return NotFound();
            }

            _context.Videojuegos.Remove(videojuego);
            await _context.SaveChangesAsync();

            return Ok($"Videojuego eliminado con éxito");
        }

        private bool VideojuegoExists(int id)
        {
            return (_context.Videojuegos?.Any(e => e.IdJuego == id)).GetValueOrDefault();
        }
    }
}
