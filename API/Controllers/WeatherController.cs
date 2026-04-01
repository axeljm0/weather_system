using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeatherApi.Data;
using WeatherApi.Models;

namespace WeatherApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<WeatherController> _logger;

        public WeatherController(AppDbContext context, ILogger<WeatherController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/weather - Obtener todos los registros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeatherRecord>>> GetAll()
        {
            var records = await _context.WeatherRecords
                .OrderByDescending(w => w.CreatedAt)
                .ToListAsync();
            return Ok(records);
        }

        // GET: api/weather/{id} - Obtener por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<WeatherRecord>> GetById(int id)
        {
            var record = await _context.WeatherRecords.FindAsync(id);
            if (record == null)
                return NotFound(new { message = $"Registro con ID {id} no encontrado" });
            return Ok(record);
        }

        // GET: api/weather/city/{city} - Obtener por ciudad (último registro)
        [HttpGet("city/{city}")]
        public async Task<ActionResult<WeatherRecord>> GetByCity(string city)
        {
            var record = await _context.WeatherRecords
                .Where(w => w.City.ToLower() == city.ToLower())
                .OrderByDescending(w => w.CreatedAt)
                .FirstOrDefaultAsync();
                
            if (record == null)
                return NotFound(new { message = $"Ciudad '{city}' no encontrada" });
            return Ok(record);
        }

        // GET: api/weather/history/{city} - Historial de una ciudad
        [HttpGet("history/{city}")]
        public async Task<ActionResult<IEnumerable<WeatherRecord>>> GetHistory(string city)
        {
            var records = await _context.WeatherRecords
                .Where(w => w.City.ToLower() == city.ToLower())
                .OrderByDescending(w => w.CreatedAt)
                .ToListAsync();
                
            if (!records.Any())
                return NotFound(new { message = $"No hay historial para '{city}'" });
            return Ok(records);
        }

        // POST: api/weather - Crear nuevo registro
        [HttpPost]
        public async Task<ActionResult<WeatherRecord>> Create(WeatherRecord record)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            record.CreatedAt = DateTime.UtcNow;
            _context.WeatherRecords.Add(record);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetById), new { id = record.Id }, record);
        }

        // PUT: api/weather/{id} - Actualizar registro existente
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, WeatherRecord record)
        {
            if (id != record.Id)
                return BadRequest(new { message = "ID de la URL no coincide con el ID del cuerpo" });
            
            var existing = await _context.WeatherRecords.FindAsync(id);
            if (existing == null)
                return NotFound(new { message = $"Registro con ID {id} no encontrado" });
            
            existing.City = record.City;
            existing.Temperature = record.Temperature;
            existing.FeelsLike = record.FeelsLike;
            existing.Description = record.Description;
            existing.Humidity = record.Humidity;
            existing.WindSpeed = record.WindSpeed;
            existing.Pressure = record.Pressure;
            existing.Icon = record.Icon;
            existing.UpdatedAt = DateTime.UtcNow;
            
            await _context.SaveChangesAsync();
            return Ok(existing);
        }

        // DELETE: api/weather/{id} - Eliminar registro
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var record = await _context.WeatherRecords.FindAsync(id);
            if (record == null)
                return NotFound(new { message = $"Registro con ID {id} no encontrado" });
            
            _context.WeatherRecords.Remove(record);
            await _context.SaveChangesAsync();
            
            return Ok(new { message = $"Registro de {record.City} eliminado correctamente", id });
        }

        // DELETE: api/weather/city/{city} - Eliminar todos los registros de una ciudad
        [HttpDelete("city/{city}")]
        public async Task<IActionResult> DeleteByCity(string city)
        {
            var records = await _context.WeatherRecords
                .Where(w => w.City.ToLower() == city.ToLower())
                .ToListAsync();
                
            if (!records.Any())
                return NotFound(new { message = $"No hay registros para '{city}'" });
            
            _context.WeatherRecords.RemoveRange(records);
            await _context.SaveChangesAsync();
            
            return Ok(new { message = $"Se eliminaron {records.Count} registros de {city}", count = records.Count });
        }
    }
}