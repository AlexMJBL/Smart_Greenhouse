using Greenhouse_Data_MVC.ViewModel;
using Greenhouse_Data_MVC.Dtos;
using Greenhouse_Data_MVC.Interfaces;
using Greenhouse_Data_MVC.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using System.Threading.Tasks;

namespace Greenhouse_Data_MVC.Controllers
{
    public class FertilizerController : Controller
    {
        private readonly IFertilizerServiceProxy _fertilizerServiceProxy;
        private readonly IPlantServiceProxy _plantServiceProxy;
        private readonly ILogger<FertilizerController> _logger;

        public FertilizerController(IFertilizerServiceProxy fertilizerServiceProxy,IPlantServiceProxy plantServiceProxy ,ILogger<FertilizerController> logger)
        {
            _fertilizerServiceProxy = fertilizerServiceProxy;
            _plantServiceProxy = plantServiceProxy;
            _logger = logger;
        }

        // GET: FertilizerController
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("User requested fertilizer list");
            var fertilizers = await _fertilizerServiceProxy.GetAllAsync();
            return View(fertilizers);
        }

        // GET: FertilizerController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var fertilizer = await _fertilizerServiceProxy.GetByIdAsync(id);
            if (fertilizer == null)
            {
                _logger.LogWarning("Unable to retrieve fertilizer with id {FertilizerId}", id);
                return NotFound();
            }

            var plant = await _plantServiceProxy.GetByIdAsync(fertilizer.PlantId);
            if (plant == null)
            {
                _logger.LogWarning("Unable to retrieve plant attached to fertilizer with id {fertilizerId}", id);
                return NotFound();
            }

            var viewModel = new FertilizerViewModel
            {
                Fertilizer = fertilizer,
                Plant = plant
            };

            _logger.LogInformation("User requested fertilizer with id : {FertilizerId}", id);
            return View(viewModel);
        }

        // GET: FertilizerController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var fertilizer = await _fertilizerServiceProxy.GetByIdAsync(id);

            if (fertilizer == null)
            {
                _logger.LogWarning("Unable to retrieve fertilizer with id {FertilizerId}",id);

                return NotFound();
            }

            return View(fertilizer);
        }

        // POST: FertilizerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _fertilizerServiceProxy.DeleteAsync(id);
                _logger.LogInformation("Fertilizer with id {FertilizerId} deleted successfully", id);
                return RedirectToAction("Index");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error while deleting fertilizer with id {FertilizerId}", id);
                return Problem("An error occurred while deleting the fertilizer.");
            }

        }

        private async Task LoadPlantsAsync()
        {
            var plants = await _plantServiceProxy.GetAllAsync();
            ViewBag.Plants = plants.Select(c =>
               new SelectListItem
               {
                   Value = c.Id.ToString(),
                   Text = c.Description
               });
        }
    }
}
