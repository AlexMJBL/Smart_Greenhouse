using Greenhouse_Ressource_MVC.Dtos;
using Greenhouse_Ressource_MVC.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Greenhouse_Ressource_MVC.Controllers
{
    public class soilHumidityCategoryController : Controller
    {
        private readonly ISoilHumidityCategoryServiceProxy _soilHumidityCategoryServiceProxy;
        private readonly ILogger<soilHumidityCategoryController> _logger;

        public soilHumidityCategoryController(ISoilHumidityCategoryServiceProxy soilHumidityCategoryServiceProxy, ILogger<soilHumidityCategoryController> logger)
        {
            _soilHumidityCategoryServiceProxy = soilHumidityCategoryServiceProxy;
            _logger = logger;
        }

        // GET: SoilHumidityCategoryController
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("User requested soilHumidityCategory list");
            var soilHumidityCategorys = await _soilHumidityCategoryServiceProxy.GetAllAsync();
            return View(soilHumidityCategorys);
        }

        // GET: SoilHumidityCategoryController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var soilHumidityCategory = await _soilHumidityCategoryServiceProxy.GetByIdAsync(id);
            if (soilHumidityCategory == null)
            {
                _logger.LogWarning("Unable to retrieve soilHumidityCategory with id {soilHumidityCategoryId}", id);
                return NotFound();
            }
            _logger.LogInformation("User requested soilHumidityCategory with id : {soilHumidityCategoryId}", id);
            return View(soilHumidityCategory);
        }

        // GET: SoilHumidityCategoryController/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: SoilHumidityCategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SoilHumidityCategoryWriteDto dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid soilHumidityCategory creation attempt");
                return View(dto);
            }

            try
            {
               await _soilHumidityCategoryServiceProxy.CreateAsync(dto);
                _logger.LogInformation("SoilHumidityCategory created successfully");
                return RedirectToAction("Index");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error while creating soilHumidityCategory");

                ModelState.AddModelError(string.Empty,"An error occurred while creating the soilHumidityCategory.");

                return View(dto);
            }
        }

        // GET: SoilHumidityCategoryController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var soilHumidityCategory = await _soilHumidityCategoryServiceProxy.GetByIdAsync(id);
            
            if (soilHumidityCategory == null)
            {
                _logger.LogWarning("Unable to retrieve soilHumidityCategory with id {soilHumidityCategoryId}", id);
                return NotFound();
            }

            return View(soilHumidityCategory);
        }

        // POST: SoilHumidityCategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SoilHumidityCategoryWriteDto dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid soilHumidityCategory modification attempt");
                return View(dto);
            }
            try
            {
                await _soilHumidityCategoryServiceProxy.UpdateAsync(id,dto);
                _logger.LogInformation("SoilHumidityCategory updated successfully");
                return RedirectToAction("Index");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error while updating soilHumidityCategory with id {soilHumidityCategoryId}", id);

                ModelState.AddModelError(string.Empty, "An error occurred while updating the soilHumidityCategory.");

                return View(dto);
            }
        }

        // GET: SoilHumidityCategoryController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var soilHumidityCategory = await _soilHumidityCategoryServiceProxy.GetByIdAsync(id);

            if (soilHumidityCategory == null)
            {
                _logger.LogWarning("Unable to retrieve soilHumidityCategory with id {soilHumidityCategoryId}",id);

                return NotFound();
            }

            return View(soilHumidityCategory);
        }

        // POST: SoilHumidityCategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _soilHumidityCategoryServiceProxy.DeleteAsync(id);
                _logger.LogInformation("SoilHumidityCategory with id {soilHumidityCategoryId} deleted successfully", id);
                return RedirectToAction("Index");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error while deleting soilHumidityCategory with id {soilHumidityCategoryId}", id);
                return Problem("An error occurred while deleting the soilHumidityCategory.");
            }

        }
    }
}
