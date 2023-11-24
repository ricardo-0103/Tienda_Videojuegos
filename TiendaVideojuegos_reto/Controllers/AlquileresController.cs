using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text;
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
    public class AlquileresController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AlquileresController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Alquileres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alquiler>>> GetAlquilers()
        {
          if (_context.Alquilers == null)
          {
              return NotFound();
          }
            return await _context.Alquilers.ToListAsync();
        }

        //Conocer quienes tienen alquilado un juego y no lo han devuelto 
        // GET: api/Alquilers/Estadoo/false
        [HttpGet("Estado/{estado}")]
        public async Task<ActionResult<Alquiler>> GetEstadoClientes(Boolean estado)
        {
            if (_context.Alquilers == null)
            {
                return NotFound();
            }

            var alquileres = await _context.Alquilers
                .Include(a => a.IdClienteNavigation)
                .Include(a => a.IdJuegoNavigation)
                .Where(a => a.Estado == estado).ToListAsync();

            if (alquileres == null)
            {
                return NotFound($"No existen clientes que tengan aqluileres en estado {estado}");
            }

            // Crear una lista para almacenar los objetos anónimos con la información deseada
            var listaInfoClientes = new List<object>();

            // Iterar sobre la colección de alquileres y agregar información a la lista sobre el cliente y el videojuego
            foreach (var alquiler in alquileres)
            {

                var infoCliente = new
                {
                    idAlquiler = alquiler.IdAlquiler,
                    idCliente = alquiler.IdCliente,
                    NombreCliente = alquiler.IdClienteNavigation.Nombre,
                    ApellidosCliente = alquiler.IdClienteNavigation.Apellidos,
                    Telefono = alquiler.IdClienteNavigation.Telefono,
                    Videojuegouego = alquiler.IdJuegoNavigation.Nombre,
                    FechaEntrega = alquiler.FechaDev,
                    Estado = alquiler.Estado
                };

                listaInfoClientes.Add(infoCliente);
            }

            return Ok(listaInfoClientes);
        }

        //Conocer el cliente más frecuente
        // GET: api/Alquilers/ClienteMasFrecuente
        [HttpGet("ClienteMasFrecuente")]
        public async Task<ActionResult<Alquiler>> GetClienteMasFrecuente()
        {
            if (_context.Alquilers == null)
            {
                return NotFound();
            }

            var clientesConFrecuencia = await _context.Alquilers
                .Include(a => a.IdClienteNavigation)  // Incluir el cliente directamente
                .GroupBy(a => a.IdCliente)
                .Select(g => new
                {
                    Cliente = g.First().IdClienteNavigation,  // Acceder directamente al cliente
                    Frecuencia = g.Count()
                })
                .OrderByDescending(x => x.Frecuencia)
                .ToListAsync();

            // Obtener la frecuencia máxima
            var maxFrecuencia = clientesConFrecuencia.First().Frecuencia;

            // Filtrar clientes con la frecuencia máxima
            var clientesConMaxFrecuencia = clientesConFrecuencia
                .TakeWhile(c => c.Frecuencia == maxFrecuencia)
                .ToList();

            // Crear una lista para almacenar los objetos anónimos con la información deseada
            var listaInfoClientes = new List<object>();

            // Iterar sobre la colección de grupos y agregar la información básica sobre los clientes
            // Puede haber más de un cliente que tengan la misma frecuencia máxima
            foreach (var cliente in clientesConMaxFrecuencia)
            {

                var infoCliente = new
                {
                    idCliente = cliente.Cliente.IdCliente,
                    NombreCliente = cliente.Cliente.Nombre,
                    ApellidosCliente = cliente.Cliente.Apellidos,
                    Telefono = cliente.Cliente.Telefono,
                    ComprasRealizadas = maxFrecuencia
                };

                listaInfoClientes.Add(infoCliente);
            }

            return Ok(listaInfoClientes);
        }

        //Conocer el título de juego más rentado
        // GET: api/Alquilers/JuegoMasRentado
        [HttpGet("JuegoMasRentado")]
        public async Task<ActionResult<Alquiler>> GetJuegoMasRentado()
        {
            if (_context.Alquilers == null)
            {
                return NotFound();
            }

            var juegosMasRentados = await _context.Alquilers
                .Include(a => a.IdJuegoNavigation)  // Incluir el juego directamente
                .GroupBy(a => a.IdJuego)
                .Select(g => new
                {
                    Juego = g.First().IdJuegoNavigation,  // Acceder directamente al juego
                    Frecuencia = g.Count()
                })
                .OrderByDescending(x => x.Frecuencia)
                .ToListAsync();

            // Obtener la frecuencia máxima
            var maxFrecuencia = juegosMasRentados.First().Frecuencia;

            // Filtrar juegos con la frecuencia máxima
            var juegosConMaxFrecuencia = juegosMasRentados
                .TakeWhile(j => j.Frecuencia == maxFrecuencia)
                .ToList();

            // Crear una lista para almacenar los objetos anónimos con la información deseada
            var listaInfoJuegos = new List<object>();

            // Iterar sobre la colección de grupos y agregar la información básica sobre los clientes
            // Puede haber más de un cliente que tengan la misma frecuencia máxima
            foreach (var juego in juegosConMaxFrecuencia)
            {

                var infoJuego = new
                {
                    idVideojuego = juego.Juego.IdJuego,
                    NombreVideojuego = juego.Juego.Nombre,
                    Plataforma = juego.Juego.Plataforma,
                    Rentas = maxFrecuencia
                };

                listaInfoJuegos.Add(infoJuego);
            }

            return Ok(listaInfoJuegos);
        }

        //Consultar las ventas del día
        // GET: api/Alquilers/Ventas
        [HttpGet("Ventas")]
        public async Task<ActionResult<Alquiler>> GetVentas()
        {
            if (_context.Alquilers == null)
            {
                return NotFound();
            }

            // Obtener la fecha actual
            DateTime fechaDeHoy = DateTime.Now;

            //Filtrar alquileres por la fecha de hoy
            var alquileresHoy = await _context.Alquilers.Where(a => a.FechaIni == fechaDeHoy).ToListAsync();

            // Crear una lista para almacenar los objetos anónimos con la información deseada
            var listaVentas = new List<object>();

            var sumVentas = 0;

            // Iterar sobre la colección de alquileres y agregar la información de las ventas
            foreach (var alquiler in alquileresHoy)
            {
                var videojuego = await _context.Videojuegos.Where(v => v.IdJuego == alquiler.IdJuego).FirstAsync();

                var infoVentas = new
                {
                    nombreVideojuego = videojuego.Nombre,
                    fechaCompra = alquiler.FechaIni,
                    fechaDevolucion = alquiler.FechaDev,
                    precioTotal = alquiler.PrecioTotal
                };

                sumVentas += (int)alquiler.PrecioTotal;
                listaVentas.Add(infoVentas);
            }

            //Añadir el valor total recauddo de las ventas generadas 
            var totVentas = new
            {
                totalVentas = sumVentas
            };

            listaVentas.Add(totVentas);

            return Ok(listaVentas);
        }

        // GET: api/Alquileres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Alquiler>> GetAlquiler(int id)
        {
          if (_context.Alquilers == null)
          {
              return NotFound();
          }
            var alquiler = await _context.Alquilers.FindAsync(id);

            if (alquiler == null)
            {
                return NotFound($"Alquiler no encontrado");
            }

            return alquiler;
        }

        // PUT: api/Alquileres/Actualizar
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Actualizar")]
        public async Task<IActionResult> PutAlquiler([FromBody] Alquiler alquiler)
        {
            if (alquiler == null || alquiler.IdAlquiler <= 0)
            {
                return BadRequest();
            }

            _context.Entry(alquiler).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlquilerExists(alquiler.IdAlquiler))
                {
                    return NotFound($"No existe el alquiler con el id {alquiler.IdAlquiler}.");
                }
                else
                {
                    throw;
                }
            }

            return Ok($"Actualizado con éxito.");
        }

        // PUT: api/Alquileres/ActualizarEstado/2
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("ActualizarEstado/{idAlquiler}")]
        public async Task<IActionResult> PutEstado(int idAlquiler)
        {

            var alquiler = await _context.Alquilers.FindAsync(idAlquiler);

            if (alquiler == null)
            {
                return NotFound($"No se encontró alquiler con el id {idAlquiler}.");
            }

            alquiler.Estado = true;

            _context.Entry(alquiler).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlquilerExists(alquiler.IdAlquiler))
                {
                    return NotFound($"No existe el alquiler con el id {alquiler.IdAlquiler}.");
                }
                else
                {
                    throw;
                }
            }

            return Ok($"Actualizado con éxito.");
        }

        // Generar comprobante
        private async Task<ActionResult<Alquiler>> GenerarComprobante(Alquiler alquiler)
        {
            if (_context.Alquilers == null)
            {
                return NotFound();
            }

            var alq = await _context.Alquilers
                .Where(a => a.IdAlquiler == alquiler.IdAlquiler)
                .Include(a => a.IdClienteNavigation)
                .Include(a => a.IdJuegoNavigation)
                .FirstOrDefaultAsync();

            var comprobante = new
            {
                nombreCliente = alq.IdClienteNavigation.Nombre,
                apellidosCliente = alq.IdClienteNavigation.Apellidos,
                valorPagado = alq.PrecioTotal,
                fechaCompra = alq.FechaIni,
                fechaDevolución = alq.FechaDev,
                nombreVideojuego = alq.IdJuegoNavigation.Nombre
            };

            return Ok(comprobante);
        }

        // POST: api/Alquileres
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Alquiler>> PostAlquiler([FromBody] Alquiler alquiler)
        {
            if(_context.Alquilers == null)
            {
                return Problem("Entity set 'AppDbContext.Alquilers'  is null.");
            }

            var alq = alquiler;

            var precio_videojuego = await _context.PrecioVideojuegos.Where(p => p.IdJuego == alquiler.IdJuego).FirstAsync();

            alq.PrecioJuego = precio_videojuego.PrecioDia;

            // Restar dos fechas
            TimeSpan tiempo = alquiler.FechaDev - alquiler.FechaIni;

            // Obtener la diferencia en días como un entero
            int tiempo_dias = tiempo.Days;

            alq.PrecioTotal = tiempo_dias * alq.PrecioJuego;

            //Inicializar estado como no entregado
            alq.Estado = false;

            _context.Alquilers.Add(alq);
            await _context.SaveChangesAsync();

            return await GenerarComprobante(alq);
        }

        // DELETE: api/Alquileres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlquiler(int id)
        {
            if (_context.Alquilers == null)
            {
                return NotFound();
            }
            var alquiler = await _context.Alquilers.FindAsync(id);
            if (alquiler == null)
            {
                return NotFound();
            }

            _context.Alquilers.Remove(alquiler);
            await _context.SaveChangesAsync();

            return Ok($"Cliente eliminado con éxito");
        }

        private bool AlquilerExists(int id)
        {
            return (_context.Alquilers?.Any(e => e.IdAlquiler == id)).GetValueOrDefault();
        }
    }
}
