using Greenhouse_API.DTOs;
using Greenhouse_API.Exceptions;
using Greenhouse_API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Greenhouse_API.Controllers
{
    /// <summary>
    /// Manages humidity records for plants in the smart greenhouse system
    /// </summary>
    [Route("api/plantHumidityRecords")]
    [ApiController]
    public class PlantHumidityRecordController : ControllerBase
    {
        private readonly IPlantHumidityRecordService _plantHumidityRecordService;

        public PlantHumidityRecordController(IPlantHumidityRecordService plantHumidityRecordService)
        {
            _plantHumidityRecordService = plantHumidityRecordService;
        }

        /// <summary>
        /// Retrieves all plant humidity records
        /// </summary>
        /// <returns>List of plant humidity records</returns>
        /// <response code="200">Returns the list of plant humidity records</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PlantHumidityRecordDto>>> GetAll()
        {
            var plantHumidityRecords = await _plantHumidityRecordService.GetAllAsync();
            return Ok(plantHumidityRecords);
        }

        /// <summary>
        /// Retrieves a plant humidity record by its ID
        /// </summary>
        /// <param name="id">Plant humidity record identifier</param>
        /// <returns>The plant humidity record</returns>
        /// <response code="200">Plant humidity record found</response>
        /// <response code="404">Plant humidity record not found</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PlantHumidityRecordDto>> GetById(int id)
        {
            var plantHumidityRecord = await _plantHumidityRecordService.GetByIdAsync(id);

            if (plantHumidityRecord == null)
                return NotFound();

            return Ok(plantHumidityRecord);
        }

        /// <summary>
        /// Creates a new plant humidity record
        /// </summary>
        /// <param name="dto">Plant humidity record creation data</param>
        /// <returns>The newly created plant humidity record</returns>
        /// <response code="201">Plant humidity record created successfully</response>
        /// <response code="400">Invalid input or related entity not found</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PlantHumidityRecordDto>> Create([FromBody] PlantHumidityRecordWriteDto dto)
        {
            try
            {
                var plantHumidityRecord = await _plantHumidityRecordService.CreateAsync(dto);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = plantHumidityRecord.Id },
                    plantHumidityRecord
                );
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a plant humidity record
        /// </summary>
        /// <param name="id">Plant humidity record identifier</param>
        /// <response code="204">Plant humidity record deleted successfully</response>
        /// <response code="404">Plant humidity record not found</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _plantHumidityRecordService.DeleteAsync(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
