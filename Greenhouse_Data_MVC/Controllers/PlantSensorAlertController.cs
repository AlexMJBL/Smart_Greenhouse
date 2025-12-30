using Greenhouse_Data_MVC.Dtos;
using Greenhouse_Data_MVC.Interfaces;
using Greenhouse_Data_MVC.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Greenhouse_Data_MVC.Controllers
{
    public class plantSensorAlertController : Controller
    {
        private readonly IPlantSensorAlertServiceProxy _plantSensorAlertServiceProxy;
        private readonly IPlantServiceProxy _plantServiceProxy;
        private readonly ILogger<plantSensorAlertController> _logger;

        public plantSensorAlertController(IPlantSensorAlertServiceProxy plantSensorAlertServiceProxy,IPlantServiceProxy plantServiceProxy ,ILogger<plantSensorAlertController> logger)
        {
            _plantSensorAlertServiceProxy = plantSensorAlertServiceProxy;
            _plantServiceProxy = plantServiceProxy;
            _logger = logger;
        }

        // GET: PlantSensorAlertController
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("User requested plantSensorAlert list");
            var plantSensorAlerts = await _plantSensorAlertServiceProxy.GetAllAsync();
            return View(plantSensorAlerts);
        }

        // GET: PlantSensorAlertController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var plantSensorAlert = await _plantSensorAlertServiceProxy.GetByIdAsync(id);
            if (plantSensorAlert == null)
            {
                _logger.LogWarning("Unable to retrieve plantSensorAlert with id {plantSensorAlertId}", id);
                return NotFound();
            }

             var plant = await _plantServiceProxy.GetByIdAsync(plantSensorAlert.PlantId);
            if( plant == null)
             {
                _logger.LogWarning("Unable to retrieve plant with id {PlantId}", plantSensorAlert.PlantId);
                return NotFound();
            }

            var vm = new PlantSensorAlertViewModel
            {
                PlantSensorAlert = plantSensorAlert,
                Plant = plant
            };
            
            _logger.LogInformation("User requested plantSensorAlert with id : {plantSensorAlertId}", id);
            return View(plantSensorAlert);
        }

        // GET: PlantSensorAlertController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var plantSensorAlert = await _plantSensorAlertServiceProxy.GetByIdAsync(id);

            if (plantSensorAlert == null)
            {
                _logger.LogWarning("Unable to retrieve plantSensorAlert with id {plantSensorAlertId}",id);

                return NotFound();
            }

            return View(plantSensorAlert);
        }

        // POST: PlantSensorAlertController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _plantSensorAlertServiceProxy.DeleteAsync(id);
                _logger.LogInformation("PlantSensorAlert with id {plantSensorAlertId} deleted successfully", id);
                return RedirectToAction("Index");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error while deleting plantSensorAlert with id {plantSensorAlertId}", id);
                return Problem("An error occurred while deleting the fertilizer.");
            }
        }
    }
}
