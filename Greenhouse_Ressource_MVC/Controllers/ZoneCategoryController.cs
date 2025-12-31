using Greenhouse_Ressource_MVC.Dtos;
using Greenhouse_Ressource_MVC.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Greenhouse_Ressource_MVC.Controllers
{
    public class ZoneCategoryController : Controller
    {
        private readonly IZoneCategoryServiceProxy _zoneCategoryServiceProxy;
        private readonly ILogger<ZoneCategoryController> _logger;

        public ZoneCategoryController(IZoneCategoryServiceProxy zoneCategoryServiceProxy, ILogger<ZoneCategoryController> logger)
        {
            _zoneCategoryServiceProxy = zoneCategoryServiceProxy;
            _logger = logger;
        }

        // GET: ZoneCategoryController
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("User requested zoneCategory list");
            var zoneCategorys = await _zoneCategoryServiceProxy.GetAllAsync();
            return View(zoneCategorys);
        }

        // GET: ZoneCategoryController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var zoneCategory = await _zoneCategoryServiceProxy.GetByIdAsync(id);
            if (zoneCategory == null)
            {
                _logger.LogWarning("Unable to retrieve zoneCategory with id {zoneCategoryId}", id);
                return NotFound();
            }
            _logger.LogInformation("User requested zoneCategory with id : {zoneCategoryId}", id);
            return View(zoneCategory);
        }

        // GET: ZoneCategoryController/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: ZoneCategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ZoneCategoryWriteDto dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid zoneCategory creation attempt");
                return View(dto);
            }

            try
            {
                if (dto.PressureMinPa == null)
                    dto.PressureMinPa = 0;
                if (dto.PressureMaxPa == null)
                    dto.PressureMaxPa = 0;

                    await _zoneCategoryServiceProxy.CreateAsync(dto);
                _logger.LogInformation("ZoneCategory created successfully");
                return RedirectToAction("Index");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error while creating zoneCategory");

                ModelState.AddModelError(string.Empty,"An error occurred while creating the zoneCategory.");

                return View(dto);
            }
        }

        // GET: ZoneCategoryController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var zoneCategory = await _zoneCategoryServiceProxy.GetByIdAsync(id);
            
            if (zoneCategory == null)
            {
                _logger.LogWarning("Unable to retrieve zoneCategory with id {zoneCategoryId}", id);
                return NotFound();
            }

            var writeDto = new ZoneCategoryWriteDto
            {
                Name = zoneCategory.Name,
                HumidityMinPct = zoneCategory.HumidityMinPct,
                HumidityMaxPct = zoneCategory.HumidityMaxPct,
                LuminosityMinLux = zoneCategory.LuminosityMinLux,
                LuminosityMaxLux = zoneCategory.LuminosityMaxLux,
                TemperatureMaxC = zoneCategory.TemperatureMaxC,
                TemperatureMinC = zoneCategory.TemperatureMinC,
                PressureMaxPa = zoneCategory.PressureMaxPa,
                PressureMinPa = zoneCategory.PressureMinPa,
            };

            return View(writeDto);
        }

        // POST: ZoneCategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ZoneCategoryWriteDto dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid zoneCategory modification attempt");
                return View(dto);
            }
            try
            {
                await _zoneCategoryServiceProxy.UpdateAsync(id,dto);
                _logger.LogInformation("ZoneCategory updated successfully");
                return RedirectToAction("Index");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error while updating zoneCategory with id {zoneCategoryId}", id);

                ModelState.AddModelError(string.Empty, "An error occurred while updating the zoneCategory.");

                return View(dto);
            }
        }

        // GET: ZoneCategoryController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var zoneCategory = await _zoneCategoryServiceProxy.GetByIdAsync(id);

            if (zoneCategory == null)
            {
                _logger.LogWarning("Unable to retrieve zoneCategory with id {zoneCategoryId}",id);

                return NotFound();
            }

            return View(zoneCategory);
        }

        // POST: ZoneCategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _zoneCategoryServiceProxy.DeleteAsync(id);
                _logger.LogInformation("ZoneCategory with id {zoneCategoryId} deleted successfully", id);
                return RedirectToAction("Index");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error while deleting zoneCategory with id {zoneCategoryId}", id);
                return Problem("An error occurred while deleting the zoneCategory.");
            }
        }
    }
}
