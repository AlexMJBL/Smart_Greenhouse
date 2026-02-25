using Greenhouse_API.DTOs;
using Greenhouse_API.Exceptions;
using Greenhouse_API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Greenhouse_API.Controllers
{
    /// <summary>
    /// Manages plant sensors used in the smart greenhouse system
    /// </summary>
    [Route("api/plantsensors")]
    [ApiController]
    public class PlantSensorController : ControllerBase
    {
        private readonly IPlantSensorService _plantSensorService;

        public PlantSensorController(IPlantSensorService plantSensorService)
        {
            _plantSensorService = plantSensorService;
        }

        /// <summary>
        /// Retrieves all plant sensors
        /// </summary>
        /// <returns>List of plant sensors</returns>
        /// <response code="200">Returns the list of plant sensors</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PlantSensorDto>>> GetAll()
        {
            var sensors = await _plantSensorService.GetAllAsync();
            return Ok(sensors);
        }

        /// <summary>
        /// Retrieves a plant sensor by its ID
        /// </summary>
        /// <param name="id">Plant sensor identifier</param>
        /// <returns>The plant sensor</returns>
        /// <response code="200">Plant sensor found</response>
        /// <response code="404">Plant sensor not found</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PlantSensorDto>> GetById(int id)
        {
            var sensor = await _plantSensorService.GetByIdAsync(id);

            if (sensor == null)
                return NotFound();

            return Ok(sensor);
        }

        /// <summary>
        /// Creates a new plant sensor
        /// </summary>
        /// <param name="dto">Plant sensor creation data</param>
        /// <returns>The newly created plant sensor</returns>
        /// <response code="201">Plant sensor created successfully</response>
        /// <response code="400">Invalid input or related plant not found</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PlantSensorDto>> Create([FromBody] PlantSensorWriteDto dto)
        {
            try
            {
                var sensor = await _plantSensorService.CreateAsync(dto);

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
        /// Updates an existing plant sensor
        /// </summary>
        /// <param name="id">Plant sensor identifier</param>
        /// <param name="dto">Updated plant sensor data</param>
        /// <returns>The updated plant sensor</returns>
        /// <response code="200">Plant sensor updated successfully</response>
        /// <response code="404">Plant sensor not found</response>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PlantSensorDto>> Update(int id, [FromBody] PlantSensorWriteDto dto)
        {
            try
            {
                var updated = await _plantSensorService.UpdateAsync(id, dto);
                return Ok(updated);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a plant sensor
        /// </summary>
        /// <param name="id">Plant sensor identifier</param>
        /// <response code="204">Plant sensor deleted successfully</response>
        /// <response code="404">Plant sensor not found</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _plantSensorService.DeleteAsync(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}