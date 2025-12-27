using Greenhouse_API.DTOs;
using Greenhouse_API.Exceptions;
using Greenhouse_API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Greenhouse_API.Controllers
{
    [Route("api/zonePressureRecords")]
    [ApiController]
    public class ZonePressureRecordController : ControllerBase
    {
        private readonly IZonePressureRecordService _zonePressureRecordService;

        public ZonePressureRecordController(IZonePressureRecordService zonePressureRecordService)
        {
            _zonePressureRecordService = zonePressureRecordService;
        }

        // GET: api/zonePressureRecords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ZonePressureRecordDto>>> GetAll()
        {
            var zonePressureRecords = await _zonePressureRecordService.GetAllAsync();
            return Ok(zonePressureRecords);
        }

        // GET: api/zonePressureRecords/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ZonePressureRecordDto>> GetById(int id)
        {
            var zonePressureRecord = await _zonePressureRecordService.GetByIdAsync(id);

            if (zonePressureRecord == null)
                return NotFound();

            return Ok(zonePressureRecord);
        }

        // POST: api/zonePressureRecords
        [HttpPost]
        public async Task<ActionResult<ZonePressureRecordDto>> Create([FromBody] ZonePressureRecordWriteDto dto)
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

        // PUT: api/zonePressureRecords/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<ZonePressureRecordDto>> Update(int id, [FromBody] ZonePressureRecordWriteDto dto)
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

        // DELETE: api/zonePressureRecords/5
        [HttpDelete("{id:int}")]
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
