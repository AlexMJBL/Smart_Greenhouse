using Greenhouse_API.DTOs;
using Greenhouse_API.Exceptions;
using Greenhouse_API.Interfaces;
using Greenhouse_API.Models;
using Greenhouse_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Greenhouse_API.Controllers
{
    /// <summary>
    /// Manages zoneSensorAlerts used in the smart greenhouse system
    /// </summary>
    [Route("api/zoneSensorAlerts")]
    [ApiController]
    public class ZoneSensorAlertController : ControllerBase
    {
        private readonly IZoneSensorAlertService _zoneSensorAlertService;

        public ZoneSensorAlertController(IZoneSensorAlertService zoneSensorAlertService)
        {
            _zoneSensorAlertService = zoneSensorAlertService;
        }

        /// <summary>
        /// Retrieves all zoneSensorAlert
        /// </summary>
        /// <returns>List of zoneSensorAlerts</returns>
        /// <response code="200">Returns the list of zoneSensorAlerts</response>
        // GET: api/zoneSensorAlerts
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ZoneSensorAlertDto>>> GetAll()
        {
            var zoneSensorAlerts = await _zoneSensorAlertService.GetAllAsync();
            return Ok(zoneSensorAlerts);
        }

        /// <summary>
        /// Retrieves a zoneSensorAlert by its ID
        /// </summary>
        /// <param name="id">ZoneSensorAlert identifier</param>
        /// <returns>The zoneSensorAlert</returns>
        /// <response code="200">ZoneSensorAlert found</response>
        /// <response code="404">ZoneSensorAlert not found</response>
        // GET: api/aoneSensorAlerts/5
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ZoneSensorAlertDto>> GetById(int id)
        {
            var zoneSensorAlert = await _zoneSensorAlertService.GetByIdAsync(id);

            if (zoneSensorAlert == null)
                return NotFound();

            return Ok(zoneSensorAlert);
        }

        /// <summary>
        /// Deletes a zoneSensorAlert
        /// </summary>
        /// <param name="id">ZoneSensorAlert identifier</param>
        /// <response code="204">ZoneSensorAlert deleted successfully</response>
        /// <response code="404">ZoneSensorAlert not found</response>
        // DELETE: api/zoneSensorAlerts/5
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _zoneSensorAlertService.DeleteAsync(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
