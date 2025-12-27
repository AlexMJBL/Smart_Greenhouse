using Greenhouse_API.DTOs;
using Greenhouse_API.Exceptions;
using Greenhouse_API.Interfaces;
using Greenhouse_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Greenhouse_API.Controllers
{
    [Route("api/soilHumidityCategories")]
    [ApiController]
    public class SoilHumidityCategoryController : ControllerBase
    {
        private readonly ISoilHumidityCategoryService _soilHumidityCategoryService;

        public SoilHumidityCategoryController(ISoilHumidityCategoryService soilHumidityCategoryService)
        {
            _soilHumidityCategoryService = soilHumidityCategoryService;
        }

        // GET: api/soilHumidityCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SoilHumidityCategoryDto>>> GetAll()
        {
            var soilHumidityCategories = await _soilHumidityCategoryService.GetAllAsync();
            return Ok(soilHumidityCategories);
        }

        // GET: api/soilHumidityCategories/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<SoilHumidityCategoryDto>> GetById(int id)
        {
            var soilHumidityCategory = await _soilHumidityCategoryService.GetByIdAsync(id);

            if (soilHumidityCategory == null)
                return NotFound();

            return Ok(soilHumidityCategory);
        }

        // POST: api/soilHumidityCategories
        [HttpPost]
        public async Task<ActionResult<SoilHumidityCategoryDto>> Create([FromBody] SoilHumidityCategoryWriteDto dto)
        {
            try
            {
                var soilHumidityCategory = await _soilHumidityCategoryService.CreateAsync(dto);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = soilHumidityCategory.Id },
                    soilHumidityCategory
                );
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/soilHumidityCategories/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<SoilHumidityCategoryDto>> Update(int id, [FromBody] SoilHumidityCategoryWriteDto dto)
        {
            try
            {
                var updated = await _soilHumidityCategoryService.UpdateAsync(id, dto);
                return Ok(updated);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE: api/soilHumidityCategories/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _soilHumidityCategoryService.DeleteAsync(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
