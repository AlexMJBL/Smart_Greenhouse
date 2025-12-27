using Greenhouse_API.DTOs;
using Greenhouse_API.Exceptions;
using Greenhouse_API.Interfaces;
using Greenhouse_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Greenhouse_API.Controllers
{  /// <summary>
    /// Manages watering events in the smart greenhouse system
    /// </summary>
    [Route("api/waterings")]
    [ApiController]
    public class WateringController : ControllerBase
    {
        private readonly IWateringService _wateringService;

        public WateringController(IWateringService wateringService)
        {
            _wateringService = wateringService;
        }

        /// <summary>
        /// Retrieves all watering records
        /// </summary>
        /// <returns>List of watering records</returns>
        /// <response code="200">Returns the list of watering records</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<WateringDto>>> GetAll()
        {
            var waterings = await _wateringService.GetAllAsync();
            return Ok(waterings);
        }

        /// <summary>
        /// Retrieves a watering record by its ID
        /// </summary>
        /// <param name="id">Watering identifier</param>
        /// <returns>The watering record</returns>
        /// <response code="200">Watering record found</response>
        /// <response code="404">Watering record not found</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WateringDto>> GetById(int id)
        {
            var watering = await _wateringService.GetByIdAsync(id);

            if (watering == null)
                return NotFound();

            return Ok(watering);
        }

        /// <summary>
        /// Creates a new watering record
        /// </summary>
        /// <param name="dto">Watering creation data</param>
        /// <returns>The newly created watering record</returns>
        /// <response code="201">Watering record created successfully</response>
        /// <response code="400">Invalid input or related entity not found</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<WateringDto>> Create([FromBody] WateringWriteDto dto)
        {
            try
            {
                var watering = await _wateringService.CreateAsync(dto);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = watering.Id },
                    watering
                );
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing watering record
        /// </summary>
        /// <param name="id">Watering identifier</param>
        /// <param name="dto">Updated watering data</param>
        /// <returns>The updated watering record</returns>
        /// <response code="200">Watering record updated successfully</response>
        /// <response code="404">Watering record not found</response>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WateringDto>> Update(int id, [FromBody] WateringWriteDto dto)
        {
            try
            {
                var updated = await _wateringService.UpdateAsync(id, dto);
                return Ok(updated);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a watering record
        /// </summary>
        /// <param name="id">Watering identifier</param>
        /// <response code="204">Watering record deleted successfully</response>
        /// <response code="404">Watering record not found</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _wateringService.DeleteAsync(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
