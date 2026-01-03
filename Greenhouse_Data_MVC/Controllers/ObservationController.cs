using Greenhouse_Data_MVC.Interfaces;
using Greenhouse_Data_MVC.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Greenhouse_Data_MVC.Controllers
{
    public class ObservationController : Controller
    {
        private readonly IObservationServiceProxy _observationServiceProxy;
        private readonly IPlantServiceProxy _plantServiceProxy;
        private readonly ILogger<ObservationController> _logger;

        public ObservationController(IObservationServiceProxy observationServiceProxy, IPlantServiceProxy plantServiceProxy, ILogger<ObservationController> logger)
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

            var plant = await _plantServiceProxy.GetByIdAsync(observation.PlantId);

            if( plant == null)
             {
                _logger.LogWarning("Unable to retrieve plant attached to observation with id {ObservationId}", id);
                return NotFound();
            }

            var vm = new ObservationViewModel
            {
                Observation = observation,
                Plant = plant
            };

            _logger.LogInformation("User requested observation with id : {ObservationId}", id);
            return View(vm);
        }

        // GET: ObservationController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var observation = await _observationServiceProxy.GetByIdAsync(id);

            if (observation == null)
            {
                _logger.LogWarning("Unable to retrieve observation with id {ObservationId}", id);

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
                return Problem("An error occurred while deleting the Observation.");
            }
        }
    }
}
