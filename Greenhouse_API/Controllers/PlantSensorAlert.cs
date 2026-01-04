using Greenhouse_API.DTOs;
using Greenhouse_API.Exceptions;
using Greenhouse_API.Interfaces;
using Greenhouse_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Greenhouse_API.Controllers
{
    /// <summary>
    /// Manages plantSensorAlerts used in the smart greenhouse system
    /// </summary>
    [Route("api/plantSensorAlerts")]
    [ApiController]
    public class PlantSensorAlertController : ControllerBase
    {
        private readonly IPlantSensorAlertService _plantSensorAlertService;

        public PlantSensorAlertController(IPlantSensorAlertService plantSensorAlertService)
        {
            _plantSensorAlertService = plantSensorAlertService;
        }

        /// <summary>
        /// Retrieves all plantSensorAlerts
        /// </summary>
        /// <returns>List of plantSensorAlerts</returns>
        /// <response code="200">Returns the list of plantSensorAlerts</response>
        // GET: api/plantSensorAlerts
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PlantSensorAlertDto>>> GetAll()
        {
            var plantSensorAlerts = await _plantSensorAlertService.GetAllAsync();
            return Ok(plantSensorAlerts);
        }

        /// <summary>
        /// Retrieves a plantSensorAlert by its ID
        /// </summary>
        /// <param name="id">PlantSensorAlert identifier</param>
        /// <returns>The plantSensorAlert</returns>
        /// <response code="200">PlantSensorAlert found</response>
        /// <response code="404">PlantSensorAlert not found</response>
        // GET: api/plantSensorAlerts/5
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PlantSensorAlertDto>> GetById(int id)
        {
            var plantSensorAlert = await _plantSensorAlertService.GetByIdAsync(id);

            if (plantSensorAlert == null)
                return NotFound();

            return Ok(plantSensorAlert);
        }

        /// <summary>
        /// Deletes a plantSensorAlert
        /// </summary>
        /// <param name="id">PlantSensorAlert identifier</param>
        /// <response code="204">PlantSensorAlert deleted successfully</response>
        /// <response code="404">PlantSensorAlert not found</response>
        // DELETE: api/plantSensorAlerts/5
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _plantSensorAlertService.DeleteAsync(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
