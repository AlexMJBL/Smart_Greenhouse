using Greenhouse_Data_MVC.Dtos;
using Greenhouse_Data_MVC.Interfaces;
using Greenhouse_Data_MVC.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Greenhouse_Data_MVC.Controllers
{
    public class plantHumidityRecordController : Controller
    {
        private readonly IPlantHumidityRecordServiceProxy _plantHumidityRecordServiceProxy;
        private readonly IPlantServiceProxy _plantServiceProxy;
        private readonly ILogger<plantHumidityRecordController> _logger;

        public plantHumidityRecordController(IPlantHumidityRecordServiceProxy plantHumidityRecordServiceProxy,IPlantServiceProxy plantServiceProxy ,ILogger<plantHumidityRecordController> logger)
        {
            _plantHumidityRecordServiceProxy = plantHumidityRecordServiceProxy;
            _plantServiceProxy = plantServiceProxy;
            _logger = logger;
        }

        // GET: PlantHumidityRecordController
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("User requested plantHumidityRecord list");
            var plantHumidityRecords = await _plantHumidityRecordServiceProxy.GetAllAsync();
            return View(plantHumidityRecords);
        }

        // GET: PlantHumidityRecordController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var plantHumidityRecord = await _plantHumidityRecordServiceProxy.GetByIdAsync(id);
            if (plantHumidityRecord == null)
            {
                _logger.LogWarning("Unable to retrieve plantHumidityRecord with id {plantHumidityRecordId}", id);
                return NotFound();
            }

             var plant = await _plantServiceProxy.GetByIdAsync(plantHumidityRecord.PlantId);
            if( plant == null)
             {
                _logger.LogWarning("Unable to retrieve plant with id {PlantId}", plantHumidityRecord.PlantId);
                return NotFound();
            }

            var vm = new PlantHumidityRecordViewModel
            {
                PlantHumidityRecord = plantHumidityRecord,
                Plant = plant
            };
            
            _logger.LogInformation("User requested plantHumidityRecord with id : {plantHumidityRecordId}", id);
            return View(plantHumidityRecord);
        }

        // GET: PlantHumidityRecordController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var plantHumidityRecord = await _plantHumidityRecordServiceProxy.GetByIdAsync(id);

            if (plantHumidityRecord == null)
            {
                _logger.LogWarning("Unable to retrieve plantHumidityRecord with id {plantHumidityRecordId}",id);

                return NotFound();
            }

            return View(plantHumidityRecord);
        }

        // POST: PlantHumidityRecordController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _plantHumidityRecordServiceProxy.DeleteAsync(id);
                _logger.LogInformation("PlantHumidityRecord with id {plantHumidityRecordId} deleted successfully", id);
                return RedirectToAction("Index");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error while deleting plantHumidityRecord with id {plantHumidityRecordId}", id);
                return Problem("An error occurred while deleting the plantHumidityRecord.");
            }
        }
    }
}
