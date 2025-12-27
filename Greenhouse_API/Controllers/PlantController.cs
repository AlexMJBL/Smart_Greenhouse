using Greenhouse_API.DTOs;
using Greenhouse_API.Exceptions;
using Greenhouse_API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Greenhouse_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantController : ControllerBase
    {
        private readonly IPlantService _plantservice;

        public PlantController(IPlantService plantservice)
        {
            _plantservice = plantservice;
        }

        // GET: api/plants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlantDto>>> GetAll()
        {
            var plants = await _plantservice.GetAllAsync();
            return Ok(plants);
        }

        // GET: api/plants/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PlantDto>> GetById(int id)
        {
            var plant = await _plantservice.GetByIdAsync(id);

            if (plant == null)
                return NotFound();

            return Ok(plant);
        }

        // POST: api/plants
        [HttpPost]
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

        // PUT: api/plants/5
        [HttpPut("{id:int}")]
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

        // DELETE: api/plants/5
        [HttpDelete("{id:int}")]
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
