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
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TiendaVideojuegos_reto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
          if (_context.Clientes == null)
          {
              return NotFound($"No hay clientes registrados.");
          }
          var clientes = await _context.Clientes.ToListAsync();

          var listaClientes = new List<object>();

          foreach (var cliente in clientes )
          {
              var c = new
              {
                  idCliente = cliente.IdCliente,
                  nombre = cliente.Nombre,
                  apellidos = cliente.Apellidos,
                  fecha_nacimiento = cliente.FechaNacimiento,
                  cedula = cliente.Cedula,
                  telefono = cliente.Telefono,
                  email = cliente.Email
              };

              listaClientes.Add(c);
          }

          return Ok(listaClientes);
        }

        // GET: api/Clientes/Balance/5
        [HttpGet("Balance/{id}")]
        public async Task<ActionResult<Cliente>> GetBalanceCliente(int id)
        {
            if (_context.Clientes == null)
            {
                return NotFound($"No existe el cliente con el id {id}.");
            }

            var cliente = await _context.Clientes
                .Include(c => c.Alquilers)
                .ThenInclude(a => a.IdJuegoNavigation)
                .SingleOrDefaultAsync(c => c.IdCliente == id);

            int totalGastado = 0;

            // Crear una lista para almacenar los objetos anónimos con la información deseada
            var listaBalance = new List<object>();

            foreach (var alquiler in cliente.Alquilers)
            {

                var balanceCliente = new
                {
                    nombreVideojuego = alquiler.IdJuegoNavigation.Nombre,
                    fechaIni = alquiler.FechaIni,
                    fechaDev = alquiler.FechaDev,
                    precioTotal = alquiler.PrecioTotal,
                    estado = alquiler.Estado
                };

                totalGastado += (int)alquiler.PrecioTotal;
          
                listaBalance.Add(balanceCliente);
            };

            //Añadir el valor total gastado por el cliente hasta el momento
            var totGastado = new
            {
                totalGastado = totalGastado
            };

            listaBalance.Add(totGastado);

            return Ok(listaBalance);
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
          if (_context.Clientes == null)
          {
              return NotFound();
          }
          var cliente = await _context.Clientes.FindAsync(id);

          if (cliente == null)
          {
              return NotFound($"No existe el cliente con el id {id}.");
          }

          var c = new
          {
              idCliente = cliente.IdCliente,
              nombre = cliente.Nombre,
              apellidos = cliente.Apellidos,
              fecha_nacimiento = cliente.FechaNacimiento,
              telefono = cliente.Telefono,
              cedula = cliente.Cedula,
              email = cliente.Email
          };

            return Ok(c);
        }

        private int CalcularEdad(DateTime fecha_nacimiento)
        {
            var fechaHoy = DateTime.Today;
            var edad = fechaHoy.Year - fecha_nacimiento.Year;

            if (fechaHoy.Date > fechaHoy.AddYears(-edad))  //comrpobar si este año ya ha cumplido
                edad--;

            return edad;
        }

        //GET: api/Clientes/JuegoMenosRentado
        [HttpGet("JuegoMenosRentado")]
        public async Task<ActionResult<Videojuego>> GetJuegoMenosRentadoPorEdad()
        {
            var clientes = await _context.Clientes
                .Include(c => c.Alquilers)
                .ThenInclude(a => a.IdJuegoNavigation)
                .ToListAsync();

            var juegosPorEdad = clientes
                .GroupBy(c => new { Edad = CalcularEdad(c.FechaNacimiento.Date) / 10 }) // Agrupar por rango de edades
                .Select(g => new
                {
                    Edad = g.Key.Edad * 10,
                    JuegoMenosRentado = g.SelectMany(c => c.Alquilers)
                        .GroupBy(a => a.IdJuego)
                        .OrderBy(x => x.Count())
                        .FirstOrDefault()?.First()?.IdJuegoNavigation
                })
                .OrderBy(x => x.Edad)
                .ToList();

            return Ok(juegosPorEdad);
        }

        private int CalculateAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;

            if (birthDate.Date > today.AddYears(-age))
                age--;

            return age;
        }


        // PUT: api/Clientes/Actualizar
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Actualizar")]
        public async Task<IActionResult> PutCliente([FromBody] Cliente cliente)
        {
            if(cliente == null || cliente.IdCliente <= 0)
            {
                return BadRequest();
            }

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(cliente.IdCliente))
                {
                    return NotFound($"No existe el cliente con el id {cliente.IdCliente}.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Clientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente([FromBody] Cliente cliente)
        {
          if (_context.Clientes == null)
          {
              return Problem("Entity set 'AppDbContext.Clientes'  is null.");
          }
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCliente", new { id = cliente.IdCliente }, cliente);
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            if (_context.Clientes == null)
            {
                return NotFound();
            }
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return Ok($"Cliente eliminado con éxito");
        }

        private bool ClienteExists(int id)
        {
            return (_context.Clientes?.Any(e => e.IdCliente == id)).GetValueOrDefault();
        }
    }
}
