using Greenhouse_API.DTOs;
using Greenhouse_API.Exceptions;
using Greenhouse_API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Greenhouse_API.Controllers
{
    /// <summary>
    /// Manages zones in the smart greenhouse system
    /// </summary>
    [Route("api/zones")]
    [ApiController]
    public class ZoneController : ControllerBase
    {
        private readonly IZoneService _zoneService;

        public ZoneController(IZoneService zoneService)
        {
            _zoneService = zoneService;
        }

        /// <summary>
        /// Retrieves all zones
        /// </summary>
        /// <returns>List of zones</returns>
        /// <response code="200">Returns the list of zones</response>
        // GET: api/zones
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ZoneDto>>> GetAll()
        {
            var zones = await _zoneService.GetAllAsync();
            return Ok(zones);
        }

        /// <summary>
        /// Retrieves a zone by its ID
        /// </summary>
        /// <param name="id">Zone identifier</param>
        /// <returns>The requested zone</returns>
        /// <response code="200">Zone found</response>
        /// <response code="404">Zone not found</response>
        // GET: api/zones/5
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ZoneDto>> GetById(int id)
        {
            var zone = await _zoneService.GetByIdAsync(id);

            if (zone == null)
                return NotFound();

            return Ok(zone);
        }

        /// <summary>
        /// Creates a new zone
        /// </summary>
        /// <param name="dto">Zone creation data</param>
        /// <returns>The newly created zone</returns>
        /// <response code="201">Zone created successfully</response>
        /// <response code="400">Invalid input or related entity not found</response>
        // POST: api/zones
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ZoneDto>> Create([FromBody] ZoneWriteDto dto)
        {
            try
            {
                var zone = await _zoneService.CreateAsync(dto);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = zone.Id },
                    zone
                );
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing zone
        /// </summary>
        /// <param name="id">Zone identifier</param>
        /// <param name="dto">Updated zone data</param>
        /// <returns>The updated zone</returns>
        /// <response code="200">Zone updated successfully</response>
        /// <response code="404">Zone not found</response>
        // PUT: api/zones/5
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ZoneDto>> Update(int id, [FromBody] ZoneWriteDto dto)
        {
            try
            {
                var updated = await _zoneService.UpdateAsync(id, dto);
                return Ok(updated);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a zone
        /// </summary>
        /// <param name="id">Zone identifier</param>
        /// <response code="204">Zone deleted successfully</response>
        /// <response code="404">Zone not found</response>
        // DELETE: api/zones/5
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _zoneService.DeleteAsync(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
