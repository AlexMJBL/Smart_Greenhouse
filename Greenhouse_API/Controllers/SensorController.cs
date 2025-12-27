using Greenhouse_API.DTOs;
using Greenhouse_API.Exceptions;
using Greenhouse_API.Interfaces;
using Greenhouse_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Greenhouse_API.Controllers
{
    [Route("api/sensors")]
    [ApiController]
    public class SensorController : ControllerBase
    {
        private readonly ISensorService _sensorService;

        public SensorController(ISensorService sensorService)
        {
            _sensorService = sensorService;
        }

        // GET: api/sensors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SensorDto>>> GetAll()
        {
            var fertilizers = await _sensorService.GetAllAsync();
            return Ok(fertilizers);
        }

        // GET: api/sensors/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<SensorDto>> GetById(int id)
        {
            var fertilizer = await _sensorService.GetByIdAsync(id);

            if (fertilizer == null)
                return NotFound();

            return Ok(fertilizer);
        }

        // POST: api/sensors
        [HttpPost]
        public async Task<ActionResult<SensorDto>> Create([FromBody] SensorWriteDto dto)
        {
            try
            {
                var sensor = await _sensorService.CreateAsync(dto);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = sensor.Id },
                    sensor
                );
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/sensors/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<SensorDto>> Update(int id, [FromBody] SensorWriteDto dto)
        {
            try
            {
                var updated = await _sensorService.UpdateAsync(id, dto);
                return Ok(updated);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE: api/ensors/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sensorService.DeleteAsync(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
