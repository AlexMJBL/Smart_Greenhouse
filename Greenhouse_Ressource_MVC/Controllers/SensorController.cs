using Greenhouse_Ressource_MVC.Dtos;
using Greenhouse_Ressource_MVC.Enums;
using Greenhouse_Ressource_MVC.Interfaces;
using Greenhouse_Ressource_MVC.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Greenhouse_Ressource_MVC.Controllers
{
    public class SensorController : Controller
    {
        private readonly ISensorServiceProxy _sensorServiceProxy;
        private readonly IZoneServiceProxy _zoneServiceProxy;
        private readonly ILogger<SensorController> _logger;

        public SensorController(ISensorServiceProxy sensorServiceProxy, IZoneServiceProxy zoneServiceProxy, ILogger<SensorController> logger)
        {
            _sensorServiceProxy = sensorServiceProxy;
            _zoneServiceProxy = zoneServiceProxy;
            _logger = logger;
        }

        // GET: SensorController
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("User requested sensor list");
            var sensors = await _sensorServiceProxy.GetAllAsync();
            return View(sensors);
        }

        // GET: SensorController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var sensor = await _sensorServiceProxy.GetByIdAsync(id);
            if (sensor == null)
            {
                _logger.LogWarning("Unable to retrieve sensor with id {sensorId}", id);
                return NotFound();
            }

            var zone = await _zoneServiceProxy.GetByIdAsync(sensor.ZoneId);
            if (zone == null)
            {
                _logger.LogWarning("Unable to retrieve zone attached to sensor with id {sensorId}", id);
                return NotFound();
            }

            var viewModel = new SensorDetailViewModel
            {
                Sensor = sensor,
                Zone = zone
            };

            _logger.LogInformation("User requested sensor with id : {sensorId}", id);
            return View(viewModel);
        }

        // GET: SensorController/Create
        public async Task<IActionResult> Create()
        {
            await LoadZonesAsync();
            await LoadSensorTypes();
            return View();
        }

        // POST: SensorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SensorWriteDto dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid sensor creation attempt");
                await LoadZonesAsync();
                await LoadSensorTypes();
                return View(dto);
            }

            try
            {
                await _sensorServiceProxy.CreateAsync(dto);
                _logger.LogInformation("Sensor created successfully");
                return RedirectToAction("Index");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error while creating sensor");

                ModelState.AddModelError(string.Empty, "An error occurred while creating the sensor.");

                await LoadZonesAsync();
                await LoadSensorTypes();
                return View(dto);
            }
        }

        // GET: SensorController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var sensor = await _sensorServiceProxy.GetByIdAsync(id);

            if (sensor == null)
            {
                _logger.LogWarning("Unable to retrieve sensor with id {sensorId}", id);
                return NotFound();
            }

            var writeDto = new SensorWriteDto
            {
                SensorCode = sensor.SensorCode,
                Description = sensor.Description,
                Type = sensor.Type,
                ZoneId = sensor.ZoneId,
            };

            await LoadZonesAsync();
            await LoadSensorTypes();
            return View(writeDto);
        }

        // POST: SensorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SensorWriteDto dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid sensor modification attempt");
                await LoadZonesAsync();
                await LoadSensorTypes();
                return View(dto);
            }
            try
            {
                await _sensorServiceProxy.UpdateAsync(id, dto);
                _logger.LogInformation("Sensor updated successfully");
                return RedirectToAction("Index");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error while updating sensor with id {sensorId}", id);

                ModelState.AddModelError(string.Empty, "An error occurred while updating the sensor.");

                await LoadZonesAsync();
                await LoadSensorTypes();
                return View(dto);
            }
        }

        // GET: SensorController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var sensor = await _sensorServiceProxy.GetByIdAsync(id);

            if (sensor == null)
            {
                _logger.LogWarning("Unable to retrieve sensor with id {sensorId}", id);

                return NotFound();
            }

            return View(sensor);
        }

        // POST: SensorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _sensorServiceProxy.DeleteAsync(id);
                _logger.LogInformation("Sensor with id {sensorId} deleted successfully", id);
                return RedirectToAction("Index");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error while deleting sensor with id {sensorId}", id);
                return Problem("An error occurred while deleting the sensor.");
            }

        }

        private async Task LoadZonesAsync()
        {
            var zones = await _zoneServiceProxy.GetAllAsync();

            ViewBag.zoneCategories = zones.Select(c =>
               new SelectListItem
               {
                   Value = c.Id.ToString(),
                   Text = c.Description
               });
        }


        private async Task LoadSensorTypes()
        {
            ViewBag.SensorTypes = Enum.GetValues(typeof(SensorType))
                .Cast<SensorType>()
                .Select(e => new SelectListItem
                {
                    Value = ((int)e).ToString(),
                    Text = e.ToString()
                })
                .ToList();
        }

    }
}
