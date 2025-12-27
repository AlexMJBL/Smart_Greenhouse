using Greenhouse_API.DTOs;
using Greenhouse_API.Exceptions;
using Greenhouse_API.Interfaces;
using Greenhouse_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Greenhouse_API.Controllers
{
    /// <summary>
    /// Manages soil humidity categories used to classify acceptable soil moisture levels
    /// </summary>
    [Route("api/soilHumidityCategories")]
    [ApiController]
    public class SoilHumidityCategoryController : ControllerBase
    {
        private readonly ISoilHumidityCategoryService _soilHumidityCategoryService;

        public SoilHumidityCategoryController(ISoilHumidityCategoryService soilHumidityCategoryService)
        {
            _soilHumidityCategoryService = soilHumidityCategoryService;
        }

        /// <summary>
        /// Retrieves all soil humidity categories
        /// </summary>
        /// <returns>List of soil humidity categories</returns>
        /// <response code="200">Returns the list of soil humidity categories</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SoilHumidityCategoryDto>>> GetAll()
        {
            var soilHumidityCategories = await _soilHumidityCategoryService.GetAllAsync();
            return Ok(soilHumidityCategories);
        }

        /// <summary>
        /// Retrieves a soil humidity category by its ID
        /// </summary>
        /// <param name="id">Soil humidity category identifier</param>
        /// <returns>The soil humidity category</returns>
        /// <response code="200">Soil humidity category found</response>
        /// <response code="404">Soil humidity category not found</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SoilHumidityCategoryDto>> GetById(int id)
        {
            var soilHumidityCategory = await _soilHumidityCategoryService.GetByIdAsync(id);

            if (soilHumidityCategory == null)
                return NotFound();

            return Ok(soilHumidityCategory);
        }

        /// <summary>
        /// Creates a new soil humidity category
        /// </summary>
        /// <param name="dto">Soil humidity category creation data</param>
        /// <returns>The newly created soil humidity category</returns>
        /// <response code="201">Soil humidity category created successfully</response>
        /// <response code="400">Invalid input or related entity not found</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Updates an existing soil humidity category
        /// </summary>
        /// <param name="id">Soil humidity category identifier</param>
        /// <param name="dto">Updated soil humidity category data</param>
        /// <returns>The updated soil humidity category</returns>
        /// <response code="200">Soil humidity category updated successfully</response>
        /// <response code="404">Soil humidity category not found</response>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Deletes a soil humidity category
        /// </summary>
        /// <param name="id">Soil humidity category identifier</param>
        /// <response code="204">Soil humidity category deleted successfully</response>
        /// <response code="404">Soil humidity category not found</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
