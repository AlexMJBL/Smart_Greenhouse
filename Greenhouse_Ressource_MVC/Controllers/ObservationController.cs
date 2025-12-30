using Greenhouse_Ressource_MVC.Dtos;
using Greenhouse_Ressource_MVC.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Greenhouse_Ressource_MVC.Controllers
{
    public class ObservationController : Controller
    {
        private readonly IObservationServiceProxy _observationServiceProxy;
        private readonly IPlantServiceProxy _plantServiceProxy;
        private readonly ILogger<ObservationController> _logger;

        public ObservationController(IObservationServiceProxy observationServiceProxy,IPlantServiceProxy plantServiceProxy ,ILogger<ObservationController> logger)
        {
            _observationServiceProxy = observationServiceProxy;
            _plantServiceProxy = plantServiceProxy;
            _logger = logger;
        }

        // GET: ObservationController
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("User requested observation list");
            var observations = await _observationServiceProxy.GetAllAsync();
            return View(observations);
        }

        // GET: ObservationController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var observation = await _observationServiceProxy.GetByIdAsync(id);
            if (observation == null)
            {
                _logger.LogWarning("Unable to retrieve observation with id {ObservationId}", id);
                return NotFound();
            }
            _logger.LogInformation("User requested observation with id : {ObservationId}", id);
            return View(observation);
        }

        // GET: ObservationController/Create
        public async Task<IActionResult> Create()
        {
            await LoadPlantsAsync();
            return View();
        }

        // POST: ObservationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ObservationWriteDto dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid observation creation attempt");
                await LoadPlantsAsync();
                return View(dto);
            }

            try
            {
               await _observationServiceProxy.CreateAsync(dto);
                _logger.LogInformation("Observation created successfully");
                return RedirectToAction("Index");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error while creating observation");

                ModelState.AddModelError(string.Empty,"An error occurred while creating the observation.");

                await LoadPlantsAsync();
                return View(dto);
            }
        }

        // GET: ObservationController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var observation = await _observationServiceProxy.GetByIdAsync(id);
            
            if (observation == null)
            {
                _logger.LogWarning("Unable to retrieve observation with id {ObservationId}", id);
                return NotFound();
            }

            await LoadPlantsAsync();
            return View(observation);
        }

        // POST: ObservationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ObservationWriteDto dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid observation modification attempt");
                await LoadPlantsAsync();
                return View(dto);
            }
            try
            {
                await _observationServiceProxy.UpdateAsync(id,dto);
                _logger.LogInformation("Observation updated successfully");
                return RedirectToAction("Index");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error while updating observation with id {ObservationId}", id);

                ModelState.AddModelError(string.Empty, "An error occurred while updating the observation.");

                await LoadPlantsAsync();
                return View(dto);
            }
        }

        // GET: ObservationController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var observation = await _observationServiceProxy.GetByIdAsync(id);

            if (observation == null)
            {
                _logger.LogWarning("Unable to retrieve observation with id {ObservationId}",id);

                return NotFound();
            }

            return View(observation);
        }

        // POST: ObservationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _observationServiceProxy.DeleteAsync(id);
                _logger.LogInformation("Observation with id {ObservationId} deleted successfully", id);
                return RedirectToAction("Index");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error while deleting observation with id {ObservationId}", id);
                return Problem("An error occurred while deleting the fertilizer.");
            }

        }

        private async Task LoadPlantsAsync()
        {
            var plants = await _plantServiceProxy.GetAllAsync();
            ViewBag.Plants = plants;
        }
    }
}
