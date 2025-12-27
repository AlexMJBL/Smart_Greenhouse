using Greenhouse_API.DTOs;
using Greenhouse_API.Exceptions;
using Greenhouse_API.Interfaces;
using Greenhouse_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Greenhouse_API.Controllers
{
    [Route("api/specimens")]
    [ApiController]
    public class SpecimenController : ControllerBase
    {
        private readonly ISpecimenService _specimenService;

        public SpecimenController(ISpecimenService specimenService)
        {
            _specimenService = specimenService;
        }

        // GET: api/specimens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpecimenDto>>> GetAll()
        {
            var specimens = await _specimenService.GetAllAsync();
            return Ok(specimens);
        }

        // GET: api/specimens/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<SpecimenDto>> GetById(int id)
        {
            var specimen = await _specimenService.GetByIdAsync(id);

            if (specimen == null)
                return NotFound();

            return Ok(specimen);
        }

        // POST: api/specimens
        [HttpPost]
        public async Task<ActionResult<SpecimenDto>> Create([FromBody] SpecimenWriteDto dto)
        {
            try
            {
                var specimen = await _specimenService.CreateAsync(dto);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = specimen.Id },
                    specimen
                );
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/specimens/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<SpecimenDto>> Update(int id, [FromBody] SpecimenWriteDto dto)
        {
            try
            {
                var updated = await _specimenService.UpdateAsync(id, dto);
                return Ok(updated);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE: api/specimens/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _specimenService.DeleteAsync(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
