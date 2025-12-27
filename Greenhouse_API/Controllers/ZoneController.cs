using Greenhouse_API.DTOs;
using Greenhouse_API.Exceptions;
using Greenhouse_API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Greenhouse_API.Controllers
{
    [Route("api/zones")]
    [ApiController]
    public class ZoneController : ControllerBase
    {
        private readonly IZoneService _zoneService;

        public ZoneController(IZoneService zoneService)
        {
            _zoneService = zoneService;
        }

        // GET: api/zones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ZoneDto>>> GetAll()
        {
            var zones = await _zoneService.GetAllAsync();
            return Ok(zones);
        }

        // GET: api/zones/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ZoneDto>> GetById(int id)
        {
            var zone = await _zoneService.GetByIdAsync(id);

            if (zone == null)
                return NotFound();

            return Ok(zone);
        }

        // POST: api/zones
        [HttpPost]
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

        // PUT: api/zones/5
        [HttpPut("{id:int}")]
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

        // DELETE: api/zones/5
        [HttpDelete("{id:int}")]
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
