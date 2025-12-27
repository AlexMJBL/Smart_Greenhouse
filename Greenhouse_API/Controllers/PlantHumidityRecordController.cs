using Greenhouse_API.DTOs;
using Greenhouse_API.Exceptions;
using Greenhouse_API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Greenhouse_API.Controllers
{
    [Route("api/plantHumidityRecords")]
    [ApiController]
    public class PlantHumidityRecordController : ControllerBase
    {
        private readonly IPlantHumidityRecordService _plantHumidityRecordService;

        public PlantHumidityRecordController(IPlantHumidityRecordService plantHumidityRecordService)
        {
            _plantHumidityRecordService = plantHumidityRecordService;
        }

        // GET: api/plantHumidityRecords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlantHumidityRecordDto>>> GetAll()
        {
            var plantHumidityRecords = await _plantHumidityRecordService.GetAllAsync();
            return Ok(plantHumidityRecords);
        }

        // GET: api/plantHumidityRecords/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PlantHumidityRecordDto>> GetById(int id)
        {
            var plantHumidityRecord = await _plantHumidityRecordService.GetByIdAsync(id);

            if (plantHumidityRecord == null)
                return NotFound();

            return Ok(plantHumidityRecord);
        }

        // POST: api/plantHumidityRecords
        [HttpPost]
        public async Task<ActionResult<PlantHumidityRecordDto>> Create(PlantHumidityRecordWriteDto dto)
        {
            try
            {
                var plantHumidityRecord = await _plantHumidityRecordService.CreateAsync(dto);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = plantHumidityRecord.Id },
                    plantHumidityRecord
                    );
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/plantHumidityRecords
        [HttpDelete("{int:id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _plantHumidityRecordService.DeleteAsync(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
