using Greenhouse_API.DTOs;
using Greenhouse_API.Exceptions;
using Greenhouse_API.Interfaces;
using Greenhouse_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Greenhouse_API.Controllers
{
    /// <summary>
    /// Manages sensors used in the smart greenhouse system
    /// </summary>
    [Route("api/sensors")]
    [ApiController]
    public class SensorController : ControllerBase
    {
        private readonly ISensorService _sensorService;

        public SensorController(ISensorService sensorService)
        {
            _sensorService = sensorService;
        }

        /// <summary>
        /// Retrieves all sensors
        /// </summary>
        /// <returns>List of sensors</returns>
        /// <response code="200">Returns the list of sensors</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SensorDto>>> GetAll()
        {
            var sensors = await _sensorService.GetAllAsync();
            return Ok(sensors);
        }

        /// <summary>
        /// Retrieves a sensor by its ID
        /// </summary>
        /// <param name="id">Sensor identifier</param>
        /// <returns>The sensor</returns>
        /// <response code="200">Sensor found</response>
        /// <response code="404">Sensor not found</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SensorDto>> GetById(int id)
        {
            var sensor = await _sensorService.GetByIdAsync(id);

            if (sensor == null)
                return NotFound();

            return Ok(sensor);
        }

        /// <summary>
        /// Creates a new sensor
        /// </summary>
        /// <param name="dto">Sensor creation data</param>
        /// <returns>The newly created sensor</returns>
        /// <response code="201">Sensor created successfully</response>
        /// <response code="400">Invalid input or related entity not found</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Updates an existing sensor
        /// </summary>
        /// <param name="id">Sensor identifier</param>
        /// <param name="dto">Updated sensor data</param>
        /// <returns>The updated sensor</returns>
        /// <response code="200">Sensor updated successfully</response>
        /// <response code="404">Sensor not found</response>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Deletes a sensor
        /// </summary>
        /// <param name="id">Sensor identifier</param>
        /// <response code="204">Sensor deleted successfully</response>
        /// <response code="404">Sensor not found</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
