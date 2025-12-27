using Greenhouse_API.DTOs;
using Greenhouse_API.Exceptions;
using Greenhouse_API.Interfaces;
using Greenhouse_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Greenhouse_API.Controllers
{
    [Route("api/waterings")]
    [ApiController]
    public class WateringController : ControllerBase
    {
        private readonly IWateringService _wateringService;

        public WateringController(IWateringService wateringService)
        {
            _wateringService = wateringService;
        }

        // GET: api/watering
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WateringDto>>> GetAll()
        {
            var waterings = await _wateringService.GetAllAsync();
            return Ok(waterings);
        }

        // GET: api/waterings/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<WateringDto>> GetById(int id)
        {
            var watering = await _wateringService.GetByIdAsync(id);

            if (watering == null)
                return NotFound();

            return Ok(watering);
        }

        // POST: api/waterings
        [HttpPost]
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

        // PUT: api/waterings/5
        [HttpPut("{id:int}")]
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

        // DELETE: api/waterings/5
        [HttpDelete("{id:int}")]
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
