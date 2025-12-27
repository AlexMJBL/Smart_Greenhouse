using Greenhouse_API.DTOs;
using Greenhouse_API.Exceptions;
using Greenhouse_API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Greenhouse_API.Controllers
{
    /// <summary>
    /// Manages pressure records measured in greenhouse zones
    /// </summary>
    [Route("api/zonePressureRecords")]
    [ApiController]
    public class ZonePressureRecordController : ControllerBase
    {
        private readonly IZonePressureRecordService _zonePressureRecordService;

        public ZonePressureRecordController(IZonePressureRecordService zonePressureRecordService)
        {
            _zonePressureRecordService = zonePressureRecordService;
        }

        /// <summary>
        /// Retrieves all zone pressure records
        /// </summary>
        /// <returns>List of zone pressure records</returns>
        /// <response code="200">Returns the list of pressure records</response>
        // GET: api/zonePressureRecords
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ZonePressureRecordDto>>> GetAll()
        {
            var zonePressureRecords = await _zonePressureRecordService.GetAllAsync();
            return Ok(zonePressureRecords);
        }

        /// <summary>
        /// Retrieves a zone pressure record by its ID
        /// </summary>
        /// <param name="id">Zone pressure record identifier</param>
        /// <returns>The requested pressure record</returns>
        /// <response code="200">Pressure record found</response>
        /// <response code="404">Pressure record not found</response>
        // GET: api/zonePressureRecords/5
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ZonePressureRecordDto>> GetById(int id)
        {
            var zonePressureRecord = await _zonePressureRecordService.GetByIdAsync(id);

            if (zonePressureRecord == null)
                return NotFound();

            return Ok(zonePressureRecord);
        }

        /// <summary>
        /// Creates a new zone pressure record
        /// </summary>
        /// <param name="dto">Zone pressure record creation data</param>
        /// <returns>The newly created pressure record</returns>
        /// <response code="201">Pressure record created successfully</response>
        /// <response code="400">Invalid input or related entity not found</response>
        // POST: api/zonePressureRecords
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ZonePressureRecordDto>> Create(
            [FromBody] ZonePressureRecordWriteDto dto)
        {
            try
            {
                var zonePressureRecord = await _zonePressureRecordService.CreateAsync(dto);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = zonePressureRecord.Id },
                    zonePressureRecord
                );
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing zone pressure record
        /// </summary>
        /// <param name="id">Zone pressure record identifier</param>
        /// <param name="dto">Updated pressure record data</param>
        /// <returns>The updated pressure record</returns>
        /// <response code="200">Pressure record updated successfully</response>
        /// <response code="404">Pressure record not found</response>
        // PUT: api/zonePressureRecords/5
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ZonePressureRecordDto>> Update(
            int id,
            [FromBody] ZonePressureRecordWriteDto dto)
        {
            try
            {
                var updated = await _zonePressureRecordService.UpdateAsync(id, dto);
                return Ok(updated);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a zone pressure record
        /// </summary>
        /// <param name="id">Zone pressure record identifier</param>
        /// <response code="204">Pressure record deleted successfully</response>
        /// <response code="404">Pressure record not found</response>
        // DELETE: api/zonePressureRecords/5
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _zonePressureRecordService.DeleteAsync(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
