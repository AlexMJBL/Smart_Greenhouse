using Greenhouse_API.DTOs;
using Greenhouse_API.Exceptions;
using Greenhouse_API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Greenhouse_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FertilizerController : ControllerBase
    {
        private readonly IFertilizerService _fertilizerService;

        public FertilizerController(IFertilizerService fertilizerService)
        {
            _fertilizerService = fertilizerService;
        }

        // GET: api/fertilizers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FertilizerDto>>> GetAll()
        {
            var fertilizers = await _fertilizerService.GetAllAsync();
            return Ok(fertilizers);
        }

        // GET: api/fertilizers/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<FertilizerDto>> GetById(int id)
        {
            var fertilizer = await _fertilizerService.GetByIdAsync(id);
            if (fertilizer == null)
            {
                return NotFound();
            }

            return Ok(fertilizer);
        }

        // POST: api/fertilizers
        [HttpPost]
        public async Task<ActionResult<FertilizerDto>> Create(
            [FromBody] FertilizerWriteDto dto)
        {
            try
            {
                var fertilizer = await _fertilizerService.CreateAsync(dto);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = fertilizer.Id },
                    fertilizer
                );
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/fertilizers/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<FertilizerDto>> Update(
            int id,
            [FromBody] FertilizerWriteDto dto)
        {
            try
            {
                var updated = await _fertilizerService.UpdateAsync(id, dto);
                return Ok(updated);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE: api/fertilizers/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _fertilizerService.DeleteAsync(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
