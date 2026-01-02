using Greenhouse_Ressource_MVC.Dtos;
using Greenhouse_Ressource_MVC.Interfaces;
using Greenhouse_Ressource_MVC.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Greenhouse_Ressource_MVC.Controllers
{
    public class zoneController : Controller
    {
        private readonly IZoneServiceProxy _zoneServiceProxy;
        private readonly IZoneCategoryServiceProxy _zoneCategoryServiceProxy;
        private readonly ILogger<zoneController> _logger;

        public zoneController(IZoneServiceProxy zoneServiceProxy,IZoneCategoryServiceProxy zoneCategoryServiceProxy ,ILogger<zoneController> logger)
        {
            _zoneServiceProxy = zoneServiceProxy;
            _zoneCategoryServiceProxy = zoneCategoryServiceProxy;
            _logger = logger;
        }

        // GET: ZoneController
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("User requested zone list");
            var zones = await _zoneServiceProxy.GetAllAsync();
            return View(zones);
        }

        // GET: ZoneController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var zone = await _zoneServiceProxy.GetByIdAsync(id);
            if (zone == null)
            {
                _logger.LogWarning("Unable to retrieve zone with id {zoneId}", id);
                return NotFound();
            }

            var zoneCategory = await _zoneCategoryServiceProxy.GetByIdAsync(zone.ZoneCategoryId);
                   if (zoneCategory== null)
            {
                _logger.LogWarning("Unable to retrieve zoneCategory attached to zone with id {zoneId}", id);
                return NotFound();
            }

            var viewModel = new ZoneDetailViewModel
            {
                Zone = zone,
                ZoneCategory = zoneCategory
            };

            _logger.LogInformation("User requested zone with id : {zoneId}", id);
            return View(viewModel);
        }

        // GET: ZoneController/Create
        public async Task<IActionResult> Create()
        {
            await LoadZoneCategorysAsync();
            return View();
        }

        // POST: ZoneController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ZoneWriteDto dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid zone creation attempt");
                await LoadZoneCategorysAsync();
                return View(dto);
            }

            try
            {
               await _zoneServiceProxy.CreateAsync(dto);
                _logger.LogInformation("Zone created successfully");
                return RedirectToAction("Index");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error while creating zone");

                ModelState.AddModelError(string.Empty,"An error occurred while creating the zone.");

                await LoadZoneCategorysAsync();
                return View(dto);
            }
        }

        // GET: ZoneController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var zone = await _zoneServiceProxy.GetByIdAsync(id);
            
            if (zone == null)
            {
                _logger.LogWarning("Unable to retrieve zone with id {zoneId}", id);
                return NotFound();
            }

            var writeDto = new ZoneWriteDto
            {
                Description = zone.Description,
                ZoneCategoryId = zone.ZoneCategoryId
            };

            await LoadZoneCategorysAsync();
            return View(writeDto);
        }

        // POST: ZoneController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ZoneWriteDto dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid zone modification attempt");
                await LoadZoneCategorysAsync();
                return View(dto);
            }
            try
            {
                await _zoneServiceProxy.UpdateAsync(id,dto);
                _logger.LogInformation("Zone updated successfully");
                return RedirectToAction("Index");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error while updating zone with id {zoneId}", id);

                ModelState.AddModelError(string.Empty, "An error occurred while updating the zone.");

                await LoadZoneCategorysAsync();
                return View(dto);
            }
        }

        // GET: ZoneController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var zone = await _zoneServiceProxy.GetByIdAsync(id);

            if (zone == null)
            {
                _logger.LogWarning("Unable to retrieve zone with id {zoneId}",id);

                return NotFound();
            }

            return View(zone);
        }

        // POST: ZoneController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _zoneServiceProxy.DeleteAsync(id);
                _logger.LogInformation("Zone with id {zoneId} deleted successfully", id);
                return RedirectToAction("Index");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error while deleting zone with id {zoneId}", id);
                return Problem("An error occurred while deleting the zone.");
            }

        }

        private async Task LoadZoneCategorysAsync()
        {
            var categories = await _zoneCategoryServiceProxy.GetAllAsync();

            ViewBag.SoilHumidityCategories = categories.Select(c =>
               new SelectListItem
               {
                   Value = c.Id.ToString(),
                   Text = c.Name
               });
        }
    }
}
