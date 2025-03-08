using Microsoft.AspNetCore.Mvc;
using Apianimales.Data;
using Apianimales.Models;
using Microsoft.EntityFrameworkCore;


namespace Apianimales.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AnimalController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Animal (Obtener todos los animales)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Animal>>> GetAnimales()
        {
            return await _context.Animales.ToListAsync();
        }

        // GET: api/Animal/5 (Obtener un animal por ID)
        [HttpGet("{id}")]
        public async Task<ActionResult<Animal>> GetAnimal(int id)
        {
            var animal = await _context.Animales.FindAsync(id);

            if (animal == null)
            {
                return NotFound();
            }

            return animal;
        }

        // POST: api/Animal (Crear un nuevo animal)
        [HttpPost]
        public async Task<ActionResult<Animal>> PostAnimal(Animal animal)
        {
            _context.Animales.Add(animal);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAnimal), new { id = animal.Id }, animal);
        }

        // PUT: api/Animal/5 (Actualizar un animal por ID)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnimal(int id, Animal animal)
        {
            if (id != animal.Id)
            {
                return BadRequest();
            }

            _context.Entry(animal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Animales.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Animal/5 (Eliminar un animal por ID)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimal(int id)
        {
            var animal = await _context.Animales.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }

            _context.Animales.Remove(animal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // 🔹 Consultas Específicas 🔹

        // 1. Obtener animales en peligro de extinción
        // Obtener animales en peligro de extinción
        [HttpGet("en-peligro")]
        public async Task<ActionResult<IEnumerable<Animal>>> GetAnimalesEnPeligro()
        {
            return await _context.Animales
                                 .Where(a => a.EnPeligroExtincion == true)  // Esto ya funciona bien con MariaDB (1 = true, 0 = false)
                                 .ToListAsync();
        }


        // 2. Obtener animales por continente
        [HttpGet("continente/{continente}")]
        public async Task<ActionResult<IEnumerable<Animal>>> GetAnimalesPorContinente(string continente)
        {
            return await _context.Animales.Where(a => a.Continente == continente).ToListAsync();
        }

        // 3. Obtener animales por especie
        [HttpGet("especie/{especie}")]
        public async Task<ActionResult<IEnumerable<Animal>>> GetAnimalesPorEspecie(string especie)
        {
            return await _context.Animales.Where(a => a.Especie == especie).ToListAsync();
        }

      
        // 5. Obtener animales con un peso mayor a un valor dado
        [HttpGet("peso-mayor/{peso}")]
        public async Task<ActionResult<IEnumerable<Animal>>> GetAnimalesPorPeso(decimal peso)
        {
            return await _context.Animales.Where(a => a.Peso > peso).ToListAsync();
        }

        // 🔹 Consultas con Ordenación 🔹

        // Obtener animales ordenados por nombre
        [HttpGet("ordenar/nombre")]
        public async Task<ActionResult<IEnumerable<Animal>>> GetAnimalesOrdenadosPorNombre()
        {
            return await _context.Animales.OrderBy(a => a.Nombre).ToListAsync();
        }

     
        // Obtener animales ordenados por peso
        [HttpGet("ordenar/peso")]
        public async Task<ActionResult<IEnumerable<Animal>>> GetAnimalesOrdenadosPorPeso()
        {
            return await _context.Animales.OrderByDescending(a => a.Peso).ToListAsync();
        }
    }
}