using Greenhouse_API.DTOs;
using Greenhouse_API.Exceptions;
using Greenhouse_API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Greenhouse_API.Controllers
{
    /// <summary>
    /// Manages plants in the smart greenhouse system
    /// </summary>
    [Route("api/plants")]
    [ApiController]
    public class PlantController : ControllerBase
    {
        private readonly IPlantService _plantservice;

        public PlantController(IPlantService plantservice)
        {
            _plantservice = plantservice;
        }

        /// <summary>
        /// Retrieves all plants
        /// </summary>
        /// <returns>List of plants</returns>
        /// <response code="200">Returns the list of plants</response>
        // GET: api/plants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlantDto>>> GetAll()
        {
            var plants = await _plantservice.GetAllAsync();
            return Ok(plants);
        }

        /// <summary>
        /// Retrieves a plant by its ID
        /// </summary>
        /// <param name="id">Plant identifier</param>
        /// <returns>The plant</returns>
        /// <response code="200">Plant found</response>
        /// <response code="404">Plant not found</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PlantDto>> GetById(int id)
        {
            var plant = await _plantservice.GetByIdAsync(id);

            if (plant == null)
                return NotFound();

            return Ok(plant);
        }

        /// <summary>
        /// Creates a new plant
        /// </summary>
        /// <param name="dto">Plant creation data</param>
        /// <returns>The newly created plant</returns>
        /// <response code="201">Plant created successfully</response>
        /// <response code="400">Invalid input or related entity not found</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PlantDto>> Create([FromBody] PlantWriteDto dto)
        {
            try
            {
                var plant = await _plantservice.CreateAsync(dto);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = plant.Id },
                    plant
                );
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing plant
        /// </summary>
        /// <param name="id">Plant identifier</param>
        /// <param name="dto">Updated plant data</param>
        /// <returns>The updated plant</returns>
        /// <response code="200">Plant updated successfully</response>
        /// <response code="404">Plant not found</response>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PlantDto>> Update(int id, [FromBody] PlantWriteDto dto)
        {
            try
            {
                var updated = await _plantservice.UpdateAsync(id, dto);
                return Ok(updated);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a plant
        /// </summary>
        /// <param name="id">Plant identifier</param>
        /// <response code="204">Plant deleted successfully</response>
        /// <response code="404">Plant not found</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _plantservice.DeleteAsync(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
