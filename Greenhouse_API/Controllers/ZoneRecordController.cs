using Greenhouse_API.DTOs;
using Greenhouse_API.Exceptions;
using Greenhouse_API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Greenhouse_API.Controllers
{
    [Route("api/zoneRecords")]
    [ApiController]
    public class ZoneRecordController : ControllerBase
    {
        private readonly IZoneRecordService _zoneRecordService;

        public ZoneRecordController(IZoneRecordService zoneRecordService)
        {
            _zoneRecordService = zoneRecordService;
        }

        // GET: api/zoneRecords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ZoneRecordDto>>> GetAll()
        {
            var zoneRecords = await _zoneRecordService.GetAllAsync();
            return Ok(zoneRecords);
        }

        // GET: api/zoneRecords/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ZoneRecordDto>> GetById(int id)
        {
            var zoneRecord = await _zoneRecordService.GetByIdAsync(id);

            if (zoneRecord == null)
                return NotFound();

            return Ok(zoneRecord);
        }

        // POST: api/zoneRecords
        [HttpPost]
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

        // DELETE: api/zoneRecords/5
        [HttpDelete("{id:int}")]
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
