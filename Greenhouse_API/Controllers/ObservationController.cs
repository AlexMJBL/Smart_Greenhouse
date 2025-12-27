using Greenhouse_API.DTOs;
using Greenhouse_API.Exceptions;
using Greenhouse_API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Greenhouse_API.Controllers
{
    [Route("api/observations")]
    [ApiController]
    public class ObservationController : ControllerBase
    {
        private readonly IObservationService _observationService;

        public ObservationController(IObservationService observationService)
        {
            _observationService = observationService;
        }

        // GET: api/observations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ObservationDto>>> GetAll()
        {
            var Observations = await _observationService.GetAllAsync();
            return Ok(Observations);
        }

        // GET: api/observations/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ObservationDto>> GetById(int id)
        {
            var observation = await _observationService.GetByIdAsync(id);
            if(observation == null)
            {
                return NotFound();
            }
            return Ok(observation);
        }

        // POST: api/fertilizers
        [HttpPost]
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


        // PUT: api/observations/5
        [HttpPut("{id:int}")]
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

        // DELETE: api/observations/5
        [HttpDelete("{id:int}")]
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