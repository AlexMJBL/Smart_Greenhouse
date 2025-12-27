using Greenhouse_API.DTOs;
using Greenhouse_API.Exceptions;
using Greenhouse_API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Greenhouse_API.Controllers
{
    /// <summary>
    /// Manages fertilizers used in the smart greenhouse system
    /// </summary>
    [Route("api/fertilizers")]
    [ApiController]
    public class FertilizerController : ControllerBase
    {
        private readonly IFertilizerService _fertilizerService;

        public FertilizerController(IFertilizerService fertilizerService)
        {
            _fertilizerService = fertilizerService;
        }

        /// <summary>
        /// Retrieves all fertilizers
        /// </summary>
        /// <returns>List of fertilizers</returns>
        /// <response code="200">Returns the list of fertilizers</response>
        // GET: api/fertilizers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FertilizerDto>>> GetAll()
        {
            var fertilizers = await _fertilizerService.GetAllAsync();
            return Ok(fertilizers);
        }

        /// <summary>
        /// Retrieves a fertilizer by its ID
        /// </summary>
        /// <param name="id">Fertilizer identifier</param>
        /// <returns>The fertilizer</returns>
        /// <response code="200">Fertilizer found</response>
        /// <response code="404">Fertilizer not found</response>
        // GET: api/fertilizers/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<FertilizerDto>> GetById(int id)
        {
            var fertilizer = await _fertilizerService.GetByIdAsync(id);

            if (fertilizer == null)
                return NotFound();

            return Ok(fertilizer);
        }


        /// <summary>
        /// Creates a new fertilizer
        /// </summary>
        /// <param name="dto">Fertilizer creation data</param>
        /// <returns>The newly created fertilizer</returns>
        /// <response code="201">Fertilizer created successfully</response>
        /// <response code="400">Invalid input or related entity not found</response>
        // POST: api/fertilizers
        [HttpPost]
        public async Task<ActionResult<FertilizerDto>> Create([FromBody] FertilizerWriteDto dto)
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


        /// <summary>
        /// Updates an existing fertilizer
        /// </summary>
        /// <param name="id">Fertilizer identifier</param>
        /// <param name="dto">Updated fertilizer data</param>
        /// <returns>The updated fertilizer</returns>
        /// <response code="200">Fertilizer updated successfully</response>
        /// <response code="404">Fertilizer not found</response>
        // PUT: api/fertilizers/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<FertilizerDto>> Update(int id,[FromBody] FertilizerWriteDto dto)
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

        /// <summary>
        /// Deletes a fertilizer
        /// </summary>
        /// <param name="id">Fertilizer identifier</param>
        /// <response code="204">Fertilizer deleted successfully</response>
        /// <response code="404">Fertilizer not found</response>
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
