using Greenhouse_API.DTOs;
using Greenhouse_API.Exceptions;
using Greenhouse_API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Greenhouse_API.Controllers
{
    /// <summary>
    /// Manages observations recorded in the smart greenhouse system
    /// </summary>
    [Route("api/observations")]
    [ApiController]
    public class ObservationController : ControllerBase
    {
        private readonly IObservationService _observationService;

        public ObservationController(IObservationService observationService)
        {
            _observationService = observationService;
        }

        /// <summary>
        /// Retrieves all observations
        /// </summary>
        /// <returns>List of observations</returns>
        /// <response code="200">Returns the list of observations</response>
        // GET: api/observations
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ObservationDto>>> GetAll()
        {
            var Observations = await _observationService.GetAllAsync();
            return Ok(Observations);
        }

        /// <summary>
        /// Retrieves an observation by its ID
        /// </summary>
        /// <param name="id">Observation identifier</param>
        /// <returns>The observation</returns>
        /// <response code="200">Observation found</response>
        /// <response code="404">Observation not found</response>
        // GET: api/observations/5
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ObservationDto>> GetById(int id)
        {
            var observation = await _observationService.GetByIdAsync(id);
            if(observation == null)
            {
                return NotFound();
            }
            return Ok(observation);
        }

        /// <summary>
        /// Creates a new observation
        /// </summary>
        /// <param name="dto">Observation creation data</param>
        /// <returns>The newly created observation</returns>
        /// <response code="201">Observation created successfully</response>
        /// <response code="400">Invalid input or related entity not found</response>
        // POST: api/fertilizers
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ObservationDto>> Create([FromBody] ObservationWriteDto dto)
        {
            try
            {
                var observation = await _observationService.CreateAsync(dto);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = observation.Id },
                    observation
                );
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Updates an existing observation
        /// </summary>
        /// <param name="id">Observation identifier</param>
        /// <param name="dto">Updated observation data</param>
        /// <returns>The updated observation</returns>
        /// <response code="200">Observation updated successfully</response>
        /// <response code="404">Observation not found</response>
        // PUT: api/observations/5
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ObservationDto>> Update (int id, ObservationWriteDto dto)
        {
              try
            {
                var updated = await _observationService.UpdateAsync(id, dto);
                return Ok(updated);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }   
        }

        /// <summary>
        /// Deletes an observation
        /// </summary>
        /// <param name="id">Observation identifier</param>
        /// <response code="204">Observation deleted successfully</response>
        /// <response code="404">Observation not found</response>
        // DELETE: api/observations/5
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _observationService.DeleteAsync(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}