using Greenhouse_Ressource_MVC.Dtos;
using Greenhouse_Ressource_MVC.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Greenhouse_Ressource_MVC.Controllers
{
    public class PlantController : Controller
    {
        private readonly IPlantServiceProxy _plantServiceProxy;
        private readonly ISpecimenServiceProxy _specimenServiceProxy;
        private readonly ILogger<PlantController> _logger;

        public PlantController(IPlantServiceProxy plantServiceProxy,ISpecimenServiceProxy specimenServiceProxy ,ILogger<PlantController> logger)
        {
            _plantServiceProxy = plantServiceProxy;
            _specimenServiceProxy = specimenServiceProxy;
            _logger = logger;
        }

        // GET: PlantController
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("User requested plant list");
            var plants = await _plantServiceProxy.GetAllAsync();
            return View(plants);
        }

        // GET: PlantController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var plant = await _plantServiceProxy.GetByIdAsync(id);
            if (plant == null)
            {
                _logger.LogWarning("Unable to retrieve plant with id {plantId}", id);
                return NotFound();
            }
            _logger.LogInformation("User requested plant with id : {plantId}", id);
            return View(plant);
        }

        // GET: PlantController/Create
        public async Task<IActionResult> Create()
        {
            await LoadSpecimensAsync();
            return View();
        }

        // POST: PlantController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PlantWriteDto dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid plant creation attempt");
                await LoadSpecimensAsync();
                return View(dto);
            }

            try
            {
               await _plantServiceProxy.CreateAsync(dto);
                _logger.LogInformation("Plant created successfully");
                return RedirectToAction("Index");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error while creating plant");

                ModelState.AddModelError(string.Empty,"An error occurred while creating the plant.");

                await LoadSpecimensAsync();
                return View(dto);
            }
        }

        // GET: PlantController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var plant = await _plantServiceProxy.GetByIdAsync(id);
            
            if (plant == null)
            {
                _logger.LogWarning("Unable to retrieve plant with id {plantId}", id);
                return NotFound();
            }

            await LoadSpecimensAsync();
            return View(plant);
        }

        // POST: PlantController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PlantWriteDto dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid plant modification attempt");
                await LoadSpecimensAsync();
                return View(dto);
            }
            try
            {
                await _plantServiceProxy.UpdateAsync(id,dto);
                _logger.LogInformation("Plant updated successfully");
                return RedirectToAction("Index");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error while updating plant with id {plantId}", id);

                ModelState.AddModelError(string.Empty, "An error occurred while updating the plant.");

                await LoadSpecimensAsync();
                return View(dto);
            }
        }

        // GET: PlantController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var plant = await _plantServiceProxy.GetByIdAsync(id);

            if (plant == null)
            {
                _logger.LogWarning("Unable to retrieve plant with id {plantId}",id);

                return NotFound();
            }

            return View(plant);
        }

        // POST: PlantController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _plantServiceProxy.DeleteAsync(id);
                _logger.LogInformation("Plant with id {plantId} deleted successfully", id);
                return RedirectToAction("Index");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error while deleting plant with id {plantId}", id);
                return Problem("An error occurred while deleting the fertilizer.");
            }

        }

        private async Task LoadSpecimensAsync()
        {
            var specimens = await _specimenServiceProxy.GetAllAsync();
            ViewBag.Plants = specimens;
        }
    }
}
