using Greenhouse_API.DTOs;
using Greenhouse_API.Exceptions;
using Greenhouse_API.Interfaces;
using Greenhouse_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Greenhouse_API.Controllers
{   /// <summary>
    /// Manages specimens in the smart greenhouse system
    /// </summary>
    [Route("api/specimens")]
    [ApiController]
    public class SpecimenController : ControllerBase
    {
        private readonly ISpecimenService _specimenService;

        public SpecimenController(ISpecimenService specimenService)
        {
            _specimenService = specimenService;
        }

        /// <summary>
        /// Retrieves all specimens
        /// </summary>
        /// <returns>List of specimens</returns>
        /// <response code="200">Returns the list of specimens</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SpecimenDto>>> GetAll()
        {
            var specimens = await _specimenService.GetAllAsync();
            return Ok(specimens);
        }

        /// <summary>
        /// Retrieves a specimen by its ID
        /// </summary>
        /// <param name="id">Specimen identifier</param>
        /// <returns>The specimen</returns>
        /// <response code="200">Specimen found</response>
        /// <response code="404">Specimen not found</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SpecimenDto>> GetById(int id)
        {
            var specimen = await _specimenService.GetByIdAsync(id);

            if (specimen == null)
                return NotFound();

            return Ok(specimen);
        }

        /// <summary>
        /// Creates a new specimen
        /// </summary>
        /// <param name="dto">Specimen creation data</param>
        /// <returns>The newly created specimen</returns>
        /// <response code="201">Specimen created successfully</response>
        /// <response code="400">Invalid input or related entity not found</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Updates an existing specimen
        /// </summary>
        /// <param name="id">Specimen identifier</param>
        /// <param name="dto">Updated specimen data</param>
        /// <returns>The updated specimen</returns>
        /// <response code="200">Specimen updated successfully</response>
        /// <response code="404">Specimen not found</response>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Deletes a specimen
        /// </summary>
        /// <param name="id">Specimen identifier</param>
        /// <response code="204">Specimen deleted successfully</response>
        /// <response code="404">Specimen not found</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
