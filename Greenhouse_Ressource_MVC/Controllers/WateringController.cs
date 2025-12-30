using Greenhouse_Ressource_MVC.Dtos;
using Greenhouse_Ressource_MVC.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Greenhouse_Ressource_MVC.Controllers
{
    public class WateringController : Controller
    {
        private readonly IWateringServiceProxy _wateringServiceProxy;
        private readonly IPlantServiceProxy _plantServiceProxy;
        private readonly ILogger<WateringController> _logger;

        public WateringController(IWateringServiceProxy wateringServiceProxy,IPlantServiceProxy plantServiceProxy ,ILogger<WateringController> logger)
        {
            _wateringServiceProxy = wateringServiceProxy;
            _plantServiceProxy = plantServiceProxy;
            _logger = logger;
        }

        // GET: WateringController
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("User requested watering list");
            var waterings = await _wateringServiceProxy.GetAllAsync();
            return View(waterings);
        }

        // GET: WateringController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var watering = await _wateringServiceProxy.GetByIdAsync(id);
            if (watering == null)
            {
                _logger.LogWarning("Unable to retrieve watering with id {wateringId}", id);
                return NotFound();
            }
            _logger.LogInformation("User requested watering with id : {wateringId}", id);
            return View(watering);
        }

        // GET: WateringController/Create
        public async Task<IActionResult> Create()
        {
            await LoadPlantsAsync();
            return View();
        }

        // POST: WateringController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(WateringWriteDto dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid watering creation attempt");
                await LoadPlantsAsync();
                return View(dto);
            }

            try
            {
               await _wateringServiceProxy.CreateAsync(dto);
                _logger.LogInformation("Watering created successfully");
                return RedirectToAction("Index");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error while creating watering");

                ModelState.AddModelError(string.Empty,"An error occurred while creating the watering.");

                await LoadPlantsAsync();
                return View(dto);
            }
        }

        // GET: WateringController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var watering = await _wateringServiceProxy.GetByIdAsync(id);
            
            if (watering == null)
            {
                _logger.LogWarning("Unable to retrieve watering with id {wateringId}", id);
                return NotFound();
            }

            await LoadPlantsAsync();
            return View(watering);
        }

        // POST: WateringController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, WateringWriteDto dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid watering modification attempt");
                await LoadPlantsAsync();
                return View(dto);
            }
            try
            {
                await _wateringServiceProxy.UpdateAsync(id,dto);
                _logger.LogInformation("Watering updated successfully");
                return RedirectToAction("Index");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error while updating watering with id {wateringId}", id);

                ModelState.AddModelError(string.Empty, "An error occurred while updating the watering.");

                await LoadPlantsAsync();
                return View(dto);
            }
        }

        // GET: WateringController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var watering = await _wateringServiceProxy.GetByIdAsync(id);

            if (watering == null)
            {
                _logger.LogWarning("Unable to retrieve watering with id {wateringId}",id);

                return NotFound();
            }

            return View(watering);
        }

        // POST: WateringController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _wateringServiceProxy.DeleteAsync(id);
                _logger.LogInformation("Watering with id {wateringId} deleted successfully", id);
                return RedirectToAction("Index");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error while deleting watering with id {wateringId}", id);
                return Problem("An error occurred while deleting the fertilizer.");
            }

        }

        private async Task LoadPlantsAsync()
        {
            var plants = await _plantServiceProxy.GetAllAsync();
            ViewBag.waterings = plants;
        }
    }
}
