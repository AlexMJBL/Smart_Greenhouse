using Greenhouse_API.DTOs;
using Greenhouse_API.Exceptions;
using Greenhouse_API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Greenhouse_API.Controllers
{
    /// <summary>
    /// Manages zone environmental records in the smart greenhouse system
    /// </summary>
    [Route("api/zoneRecords")]
    [ApiController]
    public class ZoneRecordController : ControllerBase
    {
        private readonly IZoneRecordService _zoneRecordService;

        public ZoneRecordController(IZoneRecordService zoneRecordService)
        {
            _zoneRecordService = zoneRecordService;
        }

        /// <summary>
        /// Retrieves all zone records
        /// </summary>
        /// <returns>List of zone records</returns>
        /// <response code="200">Returns the list of zone records</response>
        // GET: api/zoneRecords
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ZoneRecordDto>>> GetAll()
        {
            var zoneRecords = await _zoneRecordService.GetAllAsync();
            return Ok(zoneRecords);
        }

        /// <summary>
        /// Retrieves a zone record by its ID
        /// </summary>
        /// <param name="id">Zone record identifier</param>
        /// <returns>The requested zone record</returns>
        /// <response code="200">Zone record found</response>
        /// <response code="404">Zone record not found</response>
        // GET: api/zoneRecords/5
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ZoneRecordDto>> GetById(int id)
        {
            var zoneRecord = await _zoneRecordService.GetByIdAsync(id);

            if (zoneRecord == null)
                return NotFound();

            return Ok(zoneRecord);
        }

        /// <summary>
        /// Creates a new zone record
        /// </summary>
        /// <param name="dto">Zone record creation data</param>
        /// <returns>The newly created zone record</returns>
        /// <response code="201">Zone record created successfully</response>
        /// <response code="400">Invalid input or related entity not found</response>
        // POST: api/zoneRecords
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ZoneRecordDto>> Create([FromBody] ZoneRecordWriteDto dto)
        {
            try
            {
                var zoneRecord = await _zoneRecordService.CreateAsync(dto);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = zoneRecord.Id },
                    zoneRecord
                );
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a zone record
        /// </summary>
        /// <param name="id">Zone record identifier</param>
        /// <response code="204">Zone record deleted successfully</response>
        /// <response code="404">Zone record not found</response>
        // DELETE: api/zoneRecords/5
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _zoneRecordService.DeleteAsync(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
