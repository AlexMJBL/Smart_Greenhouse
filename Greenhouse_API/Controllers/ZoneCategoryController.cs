using Greenhouse_API.DTOs;
using Greenhouse_API.Exceptions;
using Greenhouse_API.Interfaces;
using Greenhouse_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Greenhouse_API.Controllers
{
    [Route("api/zoneCategories")]
    [ApiController]
    public class ZoneCategoryController : ControllerBase
    {
        private readonly IZoneCategoryService _zoneCategoryService;

        public ZoneCategoryController(IZoneCategoryService zoneCategoryService)
        {
            _zoneCategoryService = zoneCategoryService;
        }

        // GET: api/zoneCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ZoneCategoryDto>>> GetAll()
        {
            var zoneCategories = await _zoneCategoryService.GetAllAsync();
            return Ok(zoneCategories);
        }

        // GET: api/zoneCategories/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ZoneCategoryDto>> GetById(int id)
        {
            var zoneCategory = await _zoneCategoryService.GetByIdAsync(id);

            if (zoneCategory == null)
                return NotFound();

            return Ok(zoneCategory);
        }

        // POST: api/zoneCategories
        [HttpPost]
        public async Task<ActionResult<ZoneCategoryDto>> Create([FromBody] ZoneCategoryWriteDto dto)
        {
            try
            {
                var zoneCategory = await _zoneCategoryService.CreateAsync(dto);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = zoneCategory.Id },
                    zoneCategory
                );
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/zoneCategories/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<ZoneCategoryDto>> Update(int id, [FromBody] ZoneCategoryWriteDto dto)
        {
            try
            {
                var updated = await _zoneCategoryService.UpdateAsync(id, dto);
                return Ok(updated);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE: api/zoneCategories/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _zoneCategoryService.DeleteAsync(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
