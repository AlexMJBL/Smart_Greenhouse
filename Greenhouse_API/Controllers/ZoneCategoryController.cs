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

        /// <summary>
        /// Retrieves all zone categories
        /// </summary>
        /// <returns>List of zone categories</returns>
        /// <response code="200">Returns the list of zone categories</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ZoneCategoryDto>>> GetAll()
        {
            var zoneCategories = await _zoneCategoryService.GetAllAsync();
            return Ok(zoneCategories);
        }

        /// <summary>
        /// Retrieves a zone category by its ID
        /// </summary>
        /// <param name="id">Zone category identifier</param>
        /// <returns>The zone category</returns>
        /// <response code="200">Zone category found</response>
        /// <response code="404">Zone category not found</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ZoneCategoryDto>> GetById(int id)
        {
            var zoneCategory = await _zoneCategoryService.GetByIdAsync(id);

            if (zoneCategory == null)
                return NotFound();

            return Ok(zoneCategory);
        }

        /// <summary>
        /// Creates a new zone category
        /// </summary>
        /// <param name="dto">Zone category creation data</param>
        /// <returns>The newly created zone category</returns>
        /// <response code="201">Zone category created successfully</response>
        /// <response code="400">Invalid input or related entity not found</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Updates an existing zone category
        /// </summary>
        /// <param name="id">Zone category identifier</param>
        /// <param name="dto">Updated zone category data</param>
        /// <returns>The updated zone category</returns>
        /// <response code="200">Zone category updated successfully</response>
        /// <response code="404">Zone category not found</response>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Deletes a zone category
        /// </summary>
        /// <param name="id">Zone category identifier</param>
        /// <response code="204">Zone category deleted successfully</response>
        /// <response code="404">Zone category not found</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
